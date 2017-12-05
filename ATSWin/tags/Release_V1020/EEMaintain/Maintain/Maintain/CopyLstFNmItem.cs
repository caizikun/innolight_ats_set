using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class CopyLstFNmItem : Form
    {
        public string name = "";
        public bool isCopyOK = false;
        private bool isExistFileName = false;
        ToolTip myNewTip = new ToolTip();

        string EEPROMTabPID = "-1";

        // 全局索引记住变量
        public int GlobalTempPNTypeIndex = -1;
        public int GlobalTempPNIndex = -1;
        public int GlobalTempLstFNmIndex = -1;

        DataSet GlobalDS; // 缓存

        public CopyLstFNmItem()
        {
            InitializeComponent();
        }

        public CopyLstFNmItem(DataSet DS, int tempIndex)
        {
            InitializeComponent();
            GlobalDS = DS;

            for (int i = 0; i < GlobalDS.Tables["GlobalProductionType"].Rows.Count; i++)
            {
                this.cboPNType.Items.Add(GlobalDS.Tables["GlobalProductionType"].Rows[i]["ItemName"].ToString());
            }

            cboPNType.Enabled = false;
            this.cboPN.Enabled = false;
            this.lstFileName.Enabled = false;
            this.txtFileName.Enabled = false;
            this.btnOK.Enabled = false;

            cboPN.Items.Clear();
            lstFileName.Items.Clear();
            txtFileName.Text = "";
                
            cboPNType.SelectedIndex = -1;
            cboPNType.SelectedIndex = tempIndex;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                isExistFileName = false;

                if (txtFileName.Text.ToString().Trim().Length == 0)
                {
                    MessageBox.Show("The new EEPROM file name is empty! Pls confirm!");
                }
                else if (txtFileName.Text.ToString().Trim().Length > 30)
                {
                    MessageBox.Show("The new EEPROM file name exceed default length! Pls confirm!");
                }
                else
                {
                    foreach (string temp in lstFileName.Items)
                    {
                        if (txtFileName.Text.ToString().ToUpper().Trim() == temp.ToString().ToUpper().Trim())
                        {
                            isExistFileName = true;
                            break;
                        }
                    }

                    if (isExistFileName)
                    {
                        MessageBox.Show("Error!\nThe new file name already exists! ");
                    }
                    else
                    {
                        name = txtFileName.Text.ToString().Trim();
                        isCopyOK = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ///// <summary>
        ///// 检测文本框输入
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void txtFileName_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (true)
        //    {
        //        e.Handled = false;
        //    }
        //    else
        //    {
        //        e.Handled = true;
        //    }
        //}

        private void txtFileName_Enter(object sender, EventArgs e)
        {
            myNewTip.Show("The maximum length of the new file name is 30!", txtFileName);
        }

        private void cboPNType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (GlobalTempPNTypeIndex == cboPNType.SelectedIndex)
                {
                    return;
                }

                cboPN.Items.Clear();
                lstFileName.Items.Clear();

                this.cboPN.Enabled = false;
                this.lstFileName.Enabled = false;
                this.txtFileName.Enabled = false;
                this.btnOK.Enabled = false;

                string currTypeID = "-1";
                for (int i = 0; i < GlobalDS.Tables["GlobalProductionType"].Rows.Count; i++)
                {
                    if (GlobalDS.Tables["GlobalProductionType"].Rows[i]["ItemName"].ToString().ToUpper() == cboPNType.Text.ToString().ToUpper())
                    {
                        currTypeID = GlobalDS.Tables["GlobalProductionType"].Rows[i]["ID"].ToString();
                        break;
                    }
                }
                string sqlCondition = "PID=" + currTypeID;
                DataRow[] mrDRs = GlobalDS.Tables["GlobalProductionName"].Select(sqlCondition);
                for (int i = 0; i < mrDRs.Length; i++)
                {
                    this.cboPN.Items.Add(mrDRs[i]["PN"].ToString());
                }

                if (cboPNType.SelectedIndex >= 0)
                {
                    cboPN.Enabled = true;
                    cboPN.SelectedIndex = -1;
                }

                GlobalTempPNTypeIndex = cboPNType.SelectedIndex;
                GlobalTempPNIndex = -1;
                GlobalTempLstFNmIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                GlobalDS.Tables.Clear();
            }
        }

        private void cboPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboPN.SelectedIndex >= 0)
                {
                    if (cboPN.SelectedIndex == GlobalTempPNIndex)
                    {
                        return;
                    }

                    lstFileName.Items.Clear();

                    EEPROMTabPID = getDTColumnInfo(GlobalDS.Tables["GlobalProductionName"], "ID", "PN='" + this.cboPN.Text + "'");

                    string sqlCondition = "PID=" + EEPROMTabPID;
                    DataRow[] mrDRs = GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Select(sqlCondition);

                    for (int i = 0; i < mrDRs.Length; i++)
                    {
                        this.lstFileName.Items.Add(mrDRs[i]["ItemName"].ToString());
                    }

                    if (this.cboPN.SelectedIndex != -1)
                    {
                        this.lstFileName.Enabled = true;
                        this.txtFileName.Enabled = true;
                        this.btnOK.Enabled = true;
                    }

                    GlobalTempPNIndex = cboPN.SelectedIndex;
                    GlobalTempLstFNmIndex = -1;
                }
            }
            catch (Exception ex)
            {
                GlobalDS.Tables.Clear();
                MessageBox.Show(ex.ToString());
            }
        }

        private void lstFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalTempLstFNmIndex = lstFileName.SelectedIndex;
        }

        /// <summary>
        /// 获取表的某项符合条件资料[唯一]
        /// </summary>
        /// <param name="dt">表名</param>
        /// <param name="CloumnName">CloumnName</param>
        /// <param name="filterString">条件</param>
        /// <returns></returns>
        string getDTColumnInfo(DataTable dt, string CloumnName, string filterString)
        {
            DataRow[] dr = dt.Select(filterString.Trim());
            string ReturnValue = "";
            try
            {
                if (dr.Length == 1)
                {
                    ReturnValue = dr[0][CloumnName].ToString();
                }
                else
                {
                    MessageBox.Show("发现不确定的记录值...-->共有" + dr.Length + " 条记录!");
                }
                return ReturnValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ReturnValue;
            }
        }
    }
}
