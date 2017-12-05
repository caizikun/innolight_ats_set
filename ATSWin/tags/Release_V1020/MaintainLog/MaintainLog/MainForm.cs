using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ATSDataBase;
using ATS;
using System.Runtime.InteropServices;
using System.IO;
using Authority;

namespace Maintain
{
    public partial class MainForm : Form
    {

        LoginInfoStruct myLoginInfoStruct;

        DataIO myDataIO;
        DataTable userLoginInfoDT;
        DataTable myResultDt;
        
        bool enableChange1 = false;
        bool enableChange2 = false;
        private float X;
        private float Y;

        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {

                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)(a + 0.5);
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a + 0.5);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a + 0.5);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a + 0.5); 
                
                //Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                //con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }

        }
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }

        void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                float newx = (float)Math.Round((this.Width / X ), 3);
                float newy = (float)Math.Round(((this.Height-40) / Y ), 3);
                setControls(newx, newy, this);
                //this.Text = this.Width.ToString() + " " + this.Height.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public MainForm(LoginInfoStruct pLoginInfoStruct)
        {
            myLoginInfoStruct = pLoginInfoStruct;
            InitializeComponent();
        }

        private void timerDate_Tick(object sender, EventArgs e)
        {
            try
            {
                tssTimelbl.Text = System.DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void QueryLogs_Load(object sender, EventArgs e)
        {
            this.Text = "QueryLogs (DataSource=" + myLoginInfoStruct.DBName + ")";
            cklRowState.CheckOnClick = true;
            this.Resize += new EventHandler(Form_Resize);
            X = this.Width;
            Y = this.Height - 40;
            setTag(this);
            Form_Resize(new object(), new EventArgs());//x,y可在实例化时赋值,最后这句是新加的，在MDI时有用

            ValidationRule pValidationRule = new ValidationRule();

            bool isLogonOK = pValidationRule.CheckLoginAccess(LoginAppName.QueryLogs, CheckAccess.ViewQueryLogs, myLoginInfoStruct.myAccessCode);
            
            if (isLogonOK)
            {
                if (myLoginInfoStruct.blnISDBSQLserver == false)
                {
                    myDataIO = new LocalDatabaseIO(myLoginInfoStruct.AccessFilePath);
                }
                else
                {
                    myDataIO = new ServerDatabaseIO(myLoginInfoStruct.ServerName, myLoginInfoStruct.DBName, myLoginInfoStruct.DBUser, myLoginInfoStruct.DBPassword);
                }
                rtxt.ReadOnly = true;
                formLoad();
                timerDate.Enabled = true;
            }
            else
            {
                MessageBox.Show("The current user doesn't have login permission, pls confirm!");
                this.Close();
            }

        }

        void formLoad()
        {           
            try
            {  
                TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
                sslRunMsg.Text = "Get data from server ...Pls wait...";
                this.rtxt.Text = "";
                this.rtxtLoginlog.Text = "";
                lstOPType.Items.Clear();
                cboAppType.Items.Clear();
                cboLoginName.Items.Clear();
                cklRowState.Items.Clear();
                cboAppType.Text = "";
                cboLoginName.Text = "";
                dateTimePickerStartTime.Text = "2000/01/01 12:00:00";
                dateTimePickerEndTime.Text = "2000/01/01 12:00:00";  
                Application.DoEvents();

                enableChange1 = false;
                enableChange2 = false;

                string queryStr = "Select * from UserLoginInfo";
                userLoginInfoDT = myDataIO.GetDataTable(queryStr, "UserLoginInfo");
                myResultDt = myDataIO.GetDataTable("Select * from OperationLogs", "OperationLogs");

                cboAppType.Items.Add("ALLApptypes");
                cboLoginName.Items.Add("ALLLoginNames");
                foreach (DataRow dr in userLoginInfoDT.Select(""))
                {
                    if (dr["Apptype"].ToString().Trim().Length > 0)
                    {
                        if (cboAppType.Items.Contains(dr["Apptype"].ToString().ToUpper()) == false)
                        {
                            cboAppType.Items.Add(dr["Apptype"].ToString().ToUpper());
                        }
                    }
                    if (dr["UserName"].ToString().Trim().Length > 0)
                    {
                        if (this.cboLoginName.Items.Contains(dr["UserName"].ToString().ToUpper()) == false)
                        {
                            cboLoginName.Items.Add(dr["UserName"].ToString().ToUpper());
                        }
                    }
                }
                cboAppType.SelectedIndex = 0;
                cboLoginName.SelectedIndex = 0;
                this.tabControl1.SelectedIndex = 0;
                cklRowState.Items.Add("AllStates");
                cklRowState.Items.Add("Added");
                cklRowState.Items.Add("Modified");
                cklRowState.Items.Add("Deleted");
                TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                string dateDiff = " Use:" + ts.Minutes.ToString() + " Minutes and" + ts.Seconds.ToString() + "." + ts.Milliseconds.ToString() + " Seconds";

                sslRunMsg.Text = "Get data from server successful..." + dateDiff;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                sslRunMsg.Text = "Get data from server failed...Pls check the 'Config.xml' or any connection is incorrect";
            }            
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                enableChange1 = false;
                enableChange2 = false;
                this.Enabled = false;
                lstOPType.Items.Clear();
                rtxt.Text = "";
                rtxt.Refresh();
                sslRunMsg.Text = "Start get info...Pls wait...";
                ssrRunMsg.Refresh();
                System.Threading.Thread.Sleep(30);

                string selectConditions = "";
                string PTabString = "";
                string userIDArraryStr = "";

                if (cboAppType.Text.Trim().Length > 0 && cboAppType.Text.Trim().ToUpper() != "ALLApptypes".ToUpper())
                {
                    if (PTabString.Length > 0)
                    {
                        PTabString += " and Apptype ='" + cboAppType.Text.Trim() + "'";
                    }
                    else
                    {
                        PTabString += " Apptype ='" + cboAppType.Text.Trim() + "'";

                    }
                }

                if (this.cboLoginName.Text.Trim().Length > 0 && cboLoginName.Text.Trim().ToUpper() != "ALLLoginNames".ToUpper())
                {
                    if (PTabString.Length > 0)
                    {
                        PTabString += " and UserName ='" + cboLoginName.Text.Trim() + "' ";
                    }
                    else
                    {
                        PTabString += " UserName ='" + cboLoginName.Text.Trim() + "' ";
                    }
                }

                string queryStr = PTabString;
                
                DataRow[] Drs = userLoginInfoDT.Select(PTabString);
                for (int i = 0; i < Drs.Length; i++)
                {   //ID	UserName	LoginOntime	LoginOffTime	Apptype	LoginInfo
                    userIDArraryStr += Drs[i]["ID"].ToString() + ",";
                }

                if (userIDArraryStr.Length > 0)
                {
                    userIDArraryStr = "(" + userIDArraryStr + ")";
                    selectConditions = "PID in " + userIDArraryStr; //" Where PID in " + userIDArraryStr;
                }
                else if (PTabString.Length >0)
                {
                    MessageBox.Show("未找到当前选择的AppType 或 LoginName 对应的资料!请确认查询条件...");
                    return;
                }

                string rowState = "";
                for (int i = 0; i < cklRowState.Items.Count; i++)
                {
                    if (cklRowState.GetItemChecked(i))
                    {
                        if (cklRowState.Items[i].ToString().ToUpper() == "ALLSTATES".ToUpper())
                        {
                            rowState = "";
                            break;
                        }
                        else if (cklRowState.Items[i].ToString().ToUpper() == "Added".ToUpper())
                        {
                            if (rowState.Length > 0)
                            {
                                rowState += " or Optype like '%Added%'";
                            }
                            else
                            {
                                rowState += " Optype like '%Added%'";
                            }
                        }
                        else if (cklRowState.Items[i].ToString().ToUpper() == "Deleted".ToUpper())
                        {
                            if (rowState.Length > 0)
                            {
                                rowState += " or Optype like '%Deleted%'";
                            }
                            else
                            {
                                rowState += " Optype like '%Deleted%'";
                            }
                        }
                        else if (cklRowState.Items[i].ToString().ToUpper() == "Modified".ToUpper())
                        {
                            if (rowState.Length > 0)
                            {
                                rowState += " or  Optype like '%Modified%'";
                            }
                            else
                            {
                                rowState += " Optype like '%Modified%'";
                            }
                        }
                    }

                }

                if (rowState.Length > 0)
                {
                    if (selectConditions.Length > 0)
                    {
                        selectConditions += " and (" + rowState + ")";
                    }
                }


                if (Convert.ToDateTime(this.dateTimePickerStartTime.Text) != Convert.ToDateTime("2000/01/01 12:00:00"))
                {
                    if (selectConditions.Length > 0)
                    {
                        selectConditions += " and ModifyTime >='" + this.dateTimePickerStartTime.Text + "' ";
                    }
                    else
                    {
                        selectConditions += " ModifyTime >='" + this.dateTimePickerStartTime.Text + "' ";
                    }
                }

                if (Convert.ToDateTime(this.dateTimePickerEndTime.Text) != Convert.ToDateTime("2000/01/01 12:00:00"))
                {
                    if (selectConditions.Length > 0)
                    {
                        selectConditions += " and ModifyTime <='" + this.dateTimePickerEndTime.Text + "' ";
                        //+ "' and LoginOfftime !='2000/01/01 12:00:00'";
                    }
                    else
                    {
                        selectConditions += " ModifyTime <='" + this.dateTimePickerEndTime.Text + "' ";
                    }
                }

                DataRow[] DrsResult = myResultDt.Select(selectConditions);
                string resultString = "";
                string startSeparator = "".PadRight(80, '#') + "\r\n";
                string endSeparator = "".PadRight(80, '#') + "\r\n\r\n\r\n";

                foreach (DataRow dr in DrsResult)
                {
                    if (dr["Optype"].ToString().Trim().Replace("/r/n", "").Length > 0)
                    {
                        string listStr =dr["Optype"].ToString().Trim().Replace("/r/n", "").Replace("\r\n", "");
                        lstOPType.Items.Add(listStr);
                        resultString += startSeparator + listStr + "\r\n"
                            + "================**" + dr["ModifyTime"].ToString().Trim() + "================**" + "\r\n"
                            + dr["DetailLogs"].ToString().Trim() + endSeparator;
                    }
                }

                resultString = resultString.Replace("/r/n", "");
                if (resultString.Length > 0)
                {
                    rtxt.Text = resultString;
                }
                else
                {
                    rtxt.Text = "Can't find any result!Pls Choose aother filterString...";
                }
                //Show LoginRecords-------------------------

                if (Convert.ToDateTime(this.dateTimePickerStartTime.Text) != Convert.ToDateTime("2000/01/01 12:00:00"))
                {
                    if (PTabString.Length > 0)
                    {
                        PTabString += " and LoginOntime >='" + this.dateTimePickerStartTime.Text + "' ";
                    }
                    else
                    {
                        PTabString += " LoginOntime >='" + this.dateTimePickerStartTime.Text + "' ";
                    }
                }

                if (Convert.ToDateTime(this.dateTimePickerEndTime.Text) != Convert.ToDateTime("2000/01/01 12:00:00"))
                {
                    if (PTabString.Length > 0)
                    {
                        PTabString += " and LoginOntime <='" + this.dateTimePickerEndTime.Text + "' ";
                        //+ "' and LoginOfftime !='2000/01/01 12:00:00'";
                    }
                    else
                    {
                        PTabString += " LoginOntime <='" + this.dateTimePickerEndTime.Text + "' ";

                    }
                }

                Drs = userLoginInfoDT.Select(PTabString);
                
                string LoninString = "";
                for (int i = 0; i < Drs.Length; i++)
                {   //ID	UserName	LoginOntime	LoginOffTime	Apptype	LoginInfo                   
                    LoninString += Drs[i]["Apptype"].ToString() + "-->" + Drs[i]["UserName"].ToString() + " : <" + Drs[i]["LoginOntime"].ToString() + " LogIn And " +
                        Drs[i]["LoginOffTime"].ToString() + " LogOff>;\r\n [IPLogs]-->" + Drs[i]["LoginInfo"].ToString() + "\r\n\r\n";
                }
                rtxtLoginlog.Text = LoninString;



                //------------------------------------------
                sslRunMsg.Text = "Get info finished...";
                ssrRunMsg.Refresh();
                System.Threading.Thread.Sleep(30);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                sslRunMsg.Text = "Get info failed...";
                ssrRunMsg.Refresh();
            }
            finally
            {
                this.Enabled = true;
            }
        }
        void btnState(bool isEnable)
        {
            this.btnQuery.Enabled = isEnable;
            this.btnReset.Enabled = isEnable;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            btnState(false);
            formLoad();
            btnState(true);
        }

        private void dateTimePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(this.dateTimePickerEndTime.Text) != Convert.ToDateTime("2000/01/01 12:00:00"))
            {
                enableChange2 = true;
            }
        }

        private void dateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(this.dateTimePickerStartTime.Text) != Convert.ToDateTime("2000/01/01 12:00:00"))
            {
                enableChange1 = true;
            }
        }
        private void dateTimePickerStartTime_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                dateTimePickerStartTime.Text = "2000/01/01 12:00:00";
                enableChange1 = false;
            }
            else
            {
                if (enableChange1 == false)
                {
                    dateTimePickerStartTime.Text = DateTime.Now.ToString();
                }
            }
        }

        private void dateTimePickerEndTime_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                dateTimePickerEndTime.Text = "2000/01/01 12:00:00";
                enableChange2 = false;
            }
            else
            {
                if (enableChange2 == false)
                {
                    dateTimePickerEndTime.Text = DateTime.Now.ToString();
                }
            }
        }
        
        private void cklRowState_MouseDown(object sender, MouseEventArgs e)
        {
            //cklRowState.CheckOnClick = true;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                for (int i = 0; i < cklRowState.Items.Count; i++)
                {
                    //cklRowState.SelectedIndex = i;
                    cklRowState.SetItemCheckState(i, CheckState.Unchecked);
                    //cklRowState
                }
            }
            else
            {

            }
        }
        
        private void lstOPType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstOPType.SelectedIndex != -1)
                {
                    string currLstItemStr = lstOPType.SelectedItem.ToString();
                    string searchString = "".PadRight(80, '#') + "\n" + lstOPType.SelectedItem.ToString();
                    string totalStr = rtxt.Text;
                    int count = 0;  //表示该字符串在总的字符串中出现次数.

                    int currLstItemIndex = 0;
                    for (int j = 0; j < lstOPType.Items.Count; j++)
                    {
                        if (lstOPType.Items[j].ToString().ToUpper().Trim() == currLstItemStr.Trim().ToUpper())
                        {
                            currLstItemIndex++;
                            if (lstOPType.SelectedIndex == j)
                            {
                                break;
                            }
                        }
                    }

                    int startIndex = 0;
                    int index = 0;
                    do
                    {
                        count++;
                        index = rtxt.Text.IndexOf(searchString, startIndex);
                        if (index != -1)
                        {                            
                            rtxt.Select(index, searchString.Length);  
                            startIndex = (index + 1);
                        }
                    } while (count != currLstItemIndex);

                    if (index != -1)
                    {
                        rtxt.Focus();
                        this.rtxt.SelectionStart = index;
                        this.rtxt.ScrollToCaret();
                    }
                    lstOPType.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void saveResult(string ss)
        {
            try
            {
                string saveFilePath = "";
                SaveFileDialog pSaveFileDialog = new SaveFileDialog();
                pSaveFileDialog.CheckPathExists = true;
                pSaveFileDialog.FileName = "OperationLogs.txt";
                pSaveFileDialog.Filter = "文本文件|*.txt|所有文件|*.*";
                pSaveFileDialog.RestoreDirectory = true;
                DialogResult blnISselected = pSaveFileDialog.ShowDialog();
                if (pSaveFileDialog.FileName.Length != 0 && blnISselected == DialogResult.OK)
                {
                    saveFilePath = pSaveFileDialog.FileName.Trim();

                    FileStream fs = new FileStream(saveFilePath, FileMode.Append);

                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    ss = ss.Replace("\n", "\r\n");
                    if (ss.Length > 0)
                    {
                        sw.WriteLine(ss);
                    }
                    sw.Close();
                    fs.Close();                    
                }
                else
                {
                    MessageBox.Show("Pls choose a path of the Local Accdb Path!", "The path of Accdb file is not Found!", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        
        private void tsmSelectAll_Click(object sender, EventArgs e)
        {
            rtxt.SelectAll();
        }

        private void tsmCopyAll_Click(object sender, EventArgs e)
        {
            rtxt.SelectAll();
            rtxt.Copy();
        }

        private void tsmCopySelect_Click(object sender, EventArgs e)
        {
            rtxt.Copy();
        }

        private void tsmSaveAll_Click(object sender, EventArgs e)
        {
            saveResult(rtxt.Text);
        }

        private void tsmSaveSelect_Click(object sender, EventArgs e)
        {
            saveResult(rtxt.SelectedText);
        }
    }
}
