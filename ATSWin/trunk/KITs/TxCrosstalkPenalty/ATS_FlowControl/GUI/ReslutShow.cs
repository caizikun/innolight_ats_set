using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace ATS
{
    public partial class ReslutShow : Form
    {
        private bool Resultflag = false;
        public ReslutShow()
        {
            InitializeComponent();
        }
     
        public void ShowReslut(bool Result, DataTable dtTestData, bool ShowErrorData)
        {

            Font a = new Font("GB2312", 14);//GB2312为字体名称，1为字体大小
          //  dataGridView1.Font = a;
            #region  自动调整窗体


            //SetSize(this);// 记录原始界面尺寸
            #region 记录窗体信息

            //在Form_Load里面添加:
            this.Resize += new EventHandler(Form1_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
            Form1_Resize(new object(), new EventArgs());//x,y可在实例化时赋值,最后这句是新加的，在MDI时有用

            #endregion


            #endregion
         //   dataGridView1.DataSource = null;


            // int aa = dataGridView_ErrorData.Size.Width;



            Resultflag = Result;
            if (Resultflag)
            {
                pictureBoxResult.Visible = true;
              //  dataGridView1.Visible = false;

                pictureBoxResult.Image = Properties.Resources.ok;
            }
            else
            {
                if (dtTestData != null && dtTestData.Rows.Count > 0 && ShowErrorData)
                {
                    pictureBoxResult.Visible = false;
                    //dataGridView1.Visible = true;

                    //dataGridView1.DataSource = dtTestData;
                    //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    //this.dataGridView_ErrorData.Columns[0].Width = Convert.ToInt32(dataGridView_ErrorData.Size.Width * 0.6);
                    //for (int i = 1; i < dataGridView_ErrorData.ColumnCount; i++)
                    //{
                    //    dataGridView_ErrorData.Columns[i].Width = Convert.ToInt32(dataGridView_ErrorData.Size.Width * 0.4 / (i - 1));
                    //}
                    //for (int i = 0; i < dataGridView_ErrorData.Columns.Count; i++)
                    //    dataGridView_ErrorData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    //AutoSizeColumn(dataGridView_ErrorData);
                    // dataGridView_ErrorData.Refresh();

                }
                else
                {
                    pictureBoxResult.Visible = true;
                    //dataGridView1.Visible = false;
                    pictureBoxResult.Image = ATS.Properties.Resources.ng;
                }


            }

            pictureBoxResult.Refresh();
        }
        /// <summary>
        /// 使DataGridView的列自适应宽度
        /// </summary>
        /// <param name="dgViewFiles"></param>
        private void AutoSizeColumn(DataGridView dgViewFiles)
        {
            int width = 0;
            //使列自使用宽度
            //对于DataGridView的每一个列都调整
            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {
                //将每一列都调整为自动适应模式
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgViewFiles.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgViewFiles.Size.Width)
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列 从左开始 0，1，2
            dgViewFiles.Columns[1].Frozen = true;
        }
        private void pictureBoxResult_Click(object sender, EventArgs e)
        {

           // dataGridView1.DataSource = null;
            this.Close();
        }

        private void ReslutShow_Click(object sender, EventArgs e)
        {

        }

        private void ReslutShow_FormClosed(object sender, FormClosedEventArgs e)
        {
            //dataGridView1.DataSource = null;
            //dataGridView1.Refresh();
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

        void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
            // this.Text = this.Width.ToString() + " " + this.Height.ToString();

        }

        #endregion
    }

}
