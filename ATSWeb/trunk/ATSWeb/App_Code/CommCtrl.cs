using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATSDataBase;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.Common;


/// <summary>
///CommCtrl 的摘要说明
/// </summary>
public class CommCtrl
{
    public CommCtrl()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 进行加密-->先先进行MD5加密,然后SHA-1
    /// </summary>
    /// <param name="password">待加密的密码</param>
    /// <returns>返回密文</returns>
    public string Encrypt(string password)
    {
        string str = "";
        str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower().Substring(4, 20);
        str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");
        return str;
    }
    /// <summary>
    /// 后台动态新增一个linkButton
    /// </summary>
    /// <param name="ID">指定该控件的id</param>
    /// <param name="itemName">指定该控件的显示文本</param>
    /// <param name="itemLink">设置该控件的链接属性</param>
    /// <returns>返回 null 则创建失败</returns>
    public LinkButton CreateLinkItem(string id, string itemName, string itemLink)
    {
        try
        {
            LinkButton pLinkButton = new LinkButton();

            pLinkButton.ID = id;
            pLinkButton.Text = itemName;
            pLinkButton.PostBackUrl = itemLink;
            return pLinkButton;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 为指定的TableRow 填充单元格资料
    /// </summary>
    /// <param name="cellChildCtrls">单元格子控件 []</param>
    /// <returns></returns>
    public TableRow CreateMyTableRow(Control[] cellChildCtrls)
    {
        TableRow tr = new TableRow();

        for (int i = 0; i < cellChildCtrls.Length; i++)
        {
            tr.Controls.Add(cellChildCtrls[i]);
            //TableCell tc = new TableCell();
            //tc.Width = tabSpace;
            //tr.Controls.Add(tc);
        }

        return tr;
    }
    /// <summary>
    /// 为指定的单元格填充控件
    /// </summary>
    /// <param name="cellChildCtrl">待填充控件</param>
    /// <returns></returns>
    public TableCell CreateMyTableCell(Control cellChildCtrl)
    {
        TableCell tc = new TableCell();
        tc.Controls.Add(cellChildCtrl);
        return tc;
    }
    /// <summary>
    /// 根据List创建表头文件
    /// </summary>
    /// <param name="lst">表头文件的字符串列表</param>
    /// <returns>表头文件</returns>
    public TableHeaderRow CreateMyTableHeader(List<string> lst)
    {
        TableHeaderRow thr = new TableHeaderRow();
        TableHeaderCell[] thcs = new TableHeaderCell[lst.Count];
        for (int i = 0; i < lst.Count; i++)
        {
           
            thcs[i] = new TableHeaderCell();
            thcs[i].Text = lst[i];
            thcs[i].Attributes.Add("style", "border:solid #000 1px;text-align:left;vertical-align:middle; background:#87CEFA;");
            thr.Controls.Add(thcs[i]);
        }
        return thr;
    }
    
    /// <summary>
    /// 新增一个HtmlGenericControl,并为其填充控件
    /// </summary>
    /// <param name="tagName">HtmlGenericControl的名称(eg:div,hr,tr,p等等)</param>
    /// <param name="pChildCtrl">待填充控件</param>
    /// <returns></returns>
    public HtmlGenericControl CreateHtmlGenericControl(string tagName, Control pChildCtrl)
    {
        HtmlGenericControl pHtmlGenericControl = new HtmlGenericControl();
        pHtmlGenericControl.TagName = tagName;
        pHtmlGenericControl.Controls.Add(pChildCtrl);
        return pHtmlGenericControl;
    }

    /// <summary>
    /// 新增一个HtmlGenericControl,并为其填充控件
    /// </summary>
    /// <param name="tagName">HtmlGenericControl的名称(eg:div,hr,tr,p等等)</param>
    /// <param name="pChildCtrl">待显示的信息</param>
    /// <returns></returns>
    public HtmlGenericControl CreateHtmlGenericControl(string tagName, string displayTxt)
    {
        HtmlGenericControl pHtmlGenericControl = new HtmlGenericControl();
        pHtmlGenericControl.TagName = tagName;
        pHtmlGenericControl.InnerText = displayTxt;
        return pHtmlGenericControl;
    }

    /// <summary>
    /// 创建BlockTitle
    /// </summary>
    /// <param name="itemName">需要创建的BlockItemName</param>
    /// <returns></returns>
    public HyperLink CreateHlk(string itemName, string BlockTypeID = "0")
    {
        HyperLink hlk = new HyperLink();
        try
        {
            hlk.Text = itemName;
            hlk.CssClass = "blockTitle";
            if (itemName.ToUpper() == "Home".ToUpper())
            {
                hlk.NavigateUrl = "Home.aspx";
            }
            else if (itemName.ToUpper() == "ATSPlan".ToUpper())
            {
                hlk.NavigateUrl = "~/WebFiles/TestPlan/ModuleType.aspx?BlockType=" + BlockTypeID;
            }
            else if (itemName.ToUpper() == "ProductionInfo".ToUpper())
            {
                //hlk.NavigateUrl = "Home.aspx";
                hlk.NavigateUrl = "~/WebFiles/Production/ProductionModuleTypeList.aspx?BlockType=" + BlockTypeID;
            }
            else if (itemName.ToUpper() == "MSAInfo".ToUpper())
            {
                //hlk.NavigateUrl = "Home.aspx";
                hlk.NavigateUrl = "~/WebFiles/MSAInfo/MSAModuleTypeList.aspx?BlockType=" + BlockTypeID;
            }
            else if (itemName.ToUpper() == "MCoefGroup".ToUpper())
            {
                //hlk.NavigateUrl = "Home.aspx";
                hlk.NavigateUrl = "~/WebFiles/MCoefGroup/MCoefGroupType.aspx?BlockType=" + BlockTypeID;
            }
            else if (itemName.ToUpper() == "Equipment".ToUpper())
            {
                //hlk.NavigateUrl = "Home.aspx";
                hlk.NavigateUrl = "~/WebFiles/Equipment/GlobalEquipList.aspx?BlockType=" + BlockTypeID;
            }
            else if (itemName.ToUpper() == "APPModel".ToUpper())
            {
                //hlk.NavigateUrl = "Home.aspx";
                hlk.NavigateUrl = "~/WebFiles/APPModel/GlobalModelList.aspx?BlockType=" + BlockTypeID;
            }
            else if (itemName.ToUpper() == "TestData".ToUpper())
            {
                hlk.NavigateUrl = "~/WebFiles/TestReport/TestData.aspx?BlockType=" + BlockTypeID;
            }
            else if (itemName.ToUpper() == "UserRoleFunction".ToUpper())
            {
                hlk.NavigateUrl = "~/WebFiles/UserRoleFunc/UserRoleFuncList.aspx?BlockType=" + BlockTypeID;
            }
            else if (itemName.ToUpper() == "GlobalSpecs".ToUpper())
            {
                hlk.NavigateUrl = "~/WebFiles/GlobalSpecs/GlobalSpecsList.aspx?BlockType=" + BlockTypeID;
            }
            else if (itemName.ToUpper() == "ChipINfor".ToUpper())
            {
                hlk.NavigateUrl = "~/WebFiles/Chip/ChipBaseList.aspx?BlockType=" + BlockTypeID;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return hlk;
    }
    /// <summary>
    /// 创建导航的链接
    /// </summary>
    /// <param name="blockFuncName">当前页面的BlockFucName</param>
    /// <param name="parentItem">当前页面的父项资料名称 (eg:当前页面为PNList,父项为TypeList="QSFP28",则parentItem="QSFP28")</param>
    /// <param name="blockFuncTypeID">当前的功能块ID</param>
    /// <param name="pIO">数据库连接IO</param>
    /// <returns>返回一个包含资料的Control,可直接新增使用!</returns>
    public Control CreateNaviCtrl(string blockFuncName, string parentItem, string blockFuncTypeID, DataIO pIO, out string NaviString,bool isAddNewModel = false)
    {
        NaviString = "";
        Control naviCtrl = new System.Web.UI.Control();
        int level = -1;
        DataTable typeDt = pIO.GetDataTable("select * from FunctionTable", "");

        DataRow[] drs;
        if (blockFuncName.Contains("_"))       //产品类型_QSFP28
        {
            string[] blockFuncNameArray = new string[2];
            blockFuncNameArray = blockFuncName.Split('_');
            drs = typeDt.Select("ItemName='" + blockFuncNameArray[0] + "' and BlockTypeID =" + blockFuncTypeID);
            blockFuncName = blockFuncNameArray[1];
        }
        else
        {
            drs = typeDt.Select("ItemName='" + blockFuncName + "' and BlockTypeID =" + blockFuncTypeID);
        }

        if (drs.Length >= 1)
        {
            level = Convert.ToInt32(drs[0]["BlockLevel"]);
            if (HttpContext.Current.Session["currMaxLevel_" + blockFuncTypeID] == null || level > Convert.ToInt32(HttpContext.Current.Session["currMaxLevel" + blockFuncTypeID]))
            {
                HttpContext.Current.Session["currMaxLevel_" + blockFuncTypeID] = level;
            }
            //将原有的level>currLevel的session资料销毁
            for (int m = level; m < Convert.ToInt32(HttpContext.Current.Session["currMaxLevel"]); m++)
            {
                HttpContext.Current.Session["LevelID_" + blockFuncTypeID + m + "_Page"] = null;
                HttpContext.Current.Session["txtLevelID_" + blockFuncTypeID + m] = null;
            }

            if (blockFuncName == "测试方案")
            {
                HttpContext.Current.Session["LevelID_" + blockFuncTypeID + level + "_Page"] = "";
            }
            else
            {
                HttpContext.Current.Session["LevelID_" + blockFuncTypeID + level + "_Page"] = HttpContext.Current.Request.RawUrl;
            }
           
            HttpContext.Current.Session["txtLevelID_" + blockFuncTypeID + level] = blockFuncName;

            if ((level - 1) >= 0)
            {
                if (isAddNewModel)
                {
                    HttpContext.Current.Session["LevelID_" + blockFuncTypeID + (level - 1) + "_Page"] = HttpContext.Current.Request.RawUrl;
                }
                else if (HttpContext.Current.Session["LevelID_" + blockFuncTypeID + (level - 1) + "_Page"] == null)
                {
                    HttpContext.Current.Session["LevelID_" + blockFuncTypeID + (level - 1) + "_Page"] = "";
                }
                HttpContext.Current.Session["txtLevelID_" + blockFuncTypeID + (level - 1)] = parentItem;
            }

            HyperLink[] hlk;

            drs = typeDt.Select("BlockTypeID=" + blockFuncTypeID, "BlockLevel,PID,ID");
            hlk = new HyperLink[level + 1];
            for (int i = 0; i < level + 1; i++)
            {
                hlk[i] = new HyperLink();
                hlk[i].NavigateUrl = HttpContext.Current.Session["LevelID_" + blockFuncTypeID + i + "_Page"].ToString();
                hlk[i].Text = HttpContext.Current.Session["txtLevelID_" + blockFuncTypeID + i].ToString();
               
                if (i == 0)
                {
                    NaviString += hlk[i].Text;
                    naviCtrl.Controls.Add(hlk[i]);
                }
                else
                {
                    NaviString +="   >>   "+ hlk[i].Text;
                    HtmlGenericControl hgc = new HtmlGenericControl("span");
                    hgc.InnerText = "   >>   ";
                    naviCtrl.Controls.Add(hgc);
                    naviCtrl.Controls.Add(hlk[i]);
                }
            }
        }
        return naviCtrl;
    }

    /// <summary>
    /// 返回datarow的被修改内容
    /// </summary>
    /// <param name="myDatarow">datarow</param>
    /// <param name="dt">datatow属于那个datatable名称,用于显示主要项目名称</param>
    /// <returns>string</returns>
    public string getDRChangeInfo(DataRow myDatarow, DataTable dt)
    {
        string ss2 = "";
        try
        {
            if (myDatarow.RowState == DataRowState.Added)
            {
                #region Added
                if (dt.Columns.Contains("PN"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <PN=" + myDatarow["PN"].ToString() + ">";
                }
                else if (dt.Columns.Contains("ItemName"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["ItemName"].ToString() + ">";
                }
                else if (dt.Columns.Contains("Item"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["Item"].ToString() + ">";
                }
                else if (dt.Columns.Contains("FieldName"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <FieldName=" + myDatarow["FieldName"].ToString() + ">";
                }
                else if (dt.Columns.Contains("DriveType"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <DriveType=" + myDatarow["DriveType"].ToString() + ">";
                }
                else if (dt.Columns.Contains("LoginName"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <LoginName=" + myDatarow["LoginName"].ToString() + ">";
                }
                else if (dt.Columns.Contains("RoleName"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <RoleName=" + myDatarow["RoleName"].ToString() + ">";
                }
                else
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <ID=" + myDatarow["ID"].ToString() + ">";
                }

                for (int k = 0; k < myDatarow.ItemArray.Length; k++)
                {
                    ss2 += myDatarow[k, DataRowVersion.Current].ToString() + ";";
                }
                ss2 += "\r\n";
                #endregion
            }
            else if (myDatarow.RowState == DataRowState.Modified)
            {
                #region Modifed
                string sss1 = "", sss2 = "";
                for (int k = 0; k < myDatarow.ItemArray.Length; k++)
                {
                    if (myDatarow[k, DataRowVersion.Current].ToString() != myDatarow[k, DataRowVersion.Original].ToString())
                    {
                        if (sss1.Length <= 0)
                        {
                            sss1 = "Modified--> <ID=" + myDatarow["ID"].ToString();
                            if (dt.Columns.Contains("PN"))
                            {
                                sss1 += ";PN=" + myDatarow["PN"].ToString();
                            }
                            else if (dt.Columns.Contains("ItemName"))
                            {
                                sss1 += ";ItemName=" + myDatarow["ItemName"].ToString();
                            }
                            else if (dt.Columns.Contains("Item"))
                            {
                                sss1 += ";Item=" + myDatarow["Item"].ToString();
                            }
                            else if (dt.Columns.Contains("FieldName"))
                            {
                                sss1 += ";FieldName=" + myDatarow["FieldName"].ToString();
                            }
                            else if (dt.Columns.Contains("ItemType"))
                            {
                                sss1 += ";ItemType=" + myDatarow["ItemType"].ToString();
                            }
                            else if (dt.Columns.Contains("LoginName"))
                            {
                                sss1 += ";LoginName=" + myDatarow["LoginName"].ToString();
                            }
                            else if (dt.Columns.Contains("RoleName"))
                            {
                                sss1 += ";RoleName=" + myDatarow["RoleName"].ToString();
                            }
                            sss1 += ">OriginalData:";
                        }
                        if (sss2.Length <= 0)
                        {
                            sss2 = "Modified--> <ID=" + myDatarow["ID"].ToString();
                            if (dt.Columns.Contains("PN"))
                            {
                                sss2 += ";PN=" + myDatarow["PN"].ToString();
                            }
                            else if (dt.Columns.Contains("ItemName"))
                            {
                                sss2 += ";ItemName=" + myDatarow["ItemName"].ToString();
                            }
                            else if (dt.Columns.Contains("Item"))
                            {
                                sss2 += ";Item=" + myDatarow["Item"].ToString();
                            }
                            else if (dt.Columns.Contains("FieldName"))
                            {
                                sss2 += ";FieldName=" + myDatarow["FieldName"].ToString();
                            }
                            else if (dt.Columns.Contains("ItemType"))
                            {
                                sss2 += ";ItemType=" + myDatarow["ItemType"].ToString();
                            }
                            else if (dt.Columns.Contains("LoginName"))
                            {
                                sss2 += ";LoginName=" + myDatarow["LoginName"].ToString();
                            }
                            else if (dt.Columns.Contains("RoleName"))
                            {
                                sss2 += ";RoleName=" + myDatarow["RoleName"].ToString();
                            }
                            sss2 += ">ModifiedData:";
                        }
                        sss1 += "[" + dt.Columns[k].ColumnName + "]=" + myDatarow[k, DataRowVersion.Original].ToString() + ";";
                        sss2 += "[" + dt.Columns[k].ColumnName + "]=" + myDatarow[k, DataRowVersion.Current].ToString() + ";";
                    }
                }
                if (sss1.Length > 0)
                {
                    ss2 += sss1 + "\r\n";
                }
                if (sss2.Length > 0)
                {
                    ss2 += sss2 + "\r\n";
                }
                if (ss2.Length > 0)
                {
                    ss2 += "\r\n";
                }
                #endregion
            }
            else if (myDatarow.RowState == DataRowState.Deleted)
            {
                #region Deleted
                if (dt.Columns.Contains("PN"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <PN=" + myDatarow["PN", DataRowVersion.Original].ToString() + ">";
                }
                else if (dt.Columns.Contains("ItemName"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["ItemName", DataRowVersion.Original].ToString() + ">";
                }
                else if (dt.Columns.Contains("Item"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["Item", DataRowVersion.Original].ToString() + ">";
                }
                else if (dt.Columns.Contains("FieldName"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <FieldName=" + myDatarow["FieldName", DataRowVersion.Original].ToString() + ">";
                }
                else if (dt.Columns.Contains("DriveType"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <DriveType=" + myDatarow["DriveType", DataRowVersion.Original].ToString() + ">";
                }
                else if (dt.Columns.Contains("LoginName"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <LoginName=" + myDatarow["LoginName", DataRowVersion.Original].ToString() + ">";
                }
                else if (dt.Columns.Contains("RoleName"))
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <RoleName=" + myDatarow["RoleName", DataRowVersion.Original].ToString() + ">";
                }
                else
                {
                    ss2 += myDatarow.RowState.ToString() + "--> <ID=" + myDatarow["ID", DataRowVersion.Original].ToString() + ">";
                }
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    ss2 += myDatarow[k, DataRowVersion.Original].ToString() + ";";
                }
                ss2 += "\r\n";
                #endregion
            }

            return ss2;
        }
        catch (Exception ex)
        {
            throw ex;
            //return ss;
        }
    }

    /// <summary>
    /// 返回datatable的操作log
    /// </summary>
    /// <param name="dt">准备检查的datatable</param>
    /// <param name="dtName">datatable的表名称</param>
    /// <param name="OpType">输出操作类型:"Added;Modified;Deleted"</param>
    /// <returns></returns>
    public string[] getOpLogs(DataTable dt, string dtName, out string[] OpType)
    {
        string[] tempStr = new string[3] { "", "", "" };
        OpType = new string[3] { "", "", "" };
        try
        {
            DataTable addDt = dt.GetChanges(DataRowState.Added);
            DataTable delelteDt = dt.GetChanges(DataRowState.Deleted);
            DataTable modifyDt = dt.GetChanges(DataRowState.Modified);

            if (addDt != null)
            {
                tempStr[0] += "<**" + dtName + "**>\r\n";
                OpType[0] = "Added";
                foreach (DataRow dr in addDt.Rows)
                {
                    tempStr[0] += getDRChangeInfo(dr, addDt);
                }
            }
            if (delelteDt != null)
            {
                tempStr[1] += "<**" + dtName + "**>\r\n";
                OpType[1] = "Delelted";
                foreach (DataRow dr in delelteDt.Rows)
                {
                    tempStr[1] += getDRChangeInfo(dr, delelteDt);
                }
            }
            if (modifyDt != null)
            {
                tempStr[2] += "<**" + dtName + "**>\r\n";
                OpType[2] = "Modified";
                foreach (DataRow dr in modifyDt.Rows)
                {
                    tempStr[2] += getDRChangeInfo(dr, modifyDt);
                }
            }

            return tempStr;
        }
        catch (Exception ex)
        {
            throw ex;
            //return OpLogs;
        }
    }

    public string getDTColumnInfo(DataTable dt, string CloumnName, string filterString)
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
                throw  new Exception(  dr.Length + " record existed! The number of records is not unique!");
            }
            return ReturnValue;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int currPrmtrCountExisted(DataTable mydt, string FullfilterString)
    {
        int result = -1;
        try
        {
            DataRow[] myROWS = mydt.Select(FullfilterString);
            result = myROWS.Length;
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static int getMAXColumnsItem(DataTable dt, string ColumnName, string queryCMD)
    {
        int myMaxValue = 0;
        try
        {
            DataRow[] DRs = dt.Select(queryCMD);

            for (int i = 0; i < DRs.Length; i++)
            {
                int myValue = Convert.ToInt32(DRs[i][ColumnName]);
                if (myMaxValue < myValue)
                    myMaxValue = myValue;
            }

            return myMaxValue;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool SetIgnoreFlagForDT(DataTable mydt, string delCondition)
    {
        try
        {
            DataRow[] DelRowS = mydt.Select(delCondition);
            foreach (DataRow dr in DelRowS)
            {
                if (dr.RowState == DataRowState.Added)
                {
                    dr.Delete();
                }
                else if (mydt.Columns.Contains("IgnoreFlag"))
                {
                    dr["IgnoreFlag"] = "true";
                }
                else
                {                    
                    return false;
                }
            }
            return true;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool DeleteItemForDT(DataTable mydt, string delCondition)
    {
        try
        {
            DataRow[] DelRowS = mydt.Select(delCondition);
            foreach (DataRow dr in DelRowS)
            {
                dr.Delete();
            }
            return true;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    public enum FunctionName
    {
        //ATSPlan = 1, Production = 2, MSA = 3, MCoefInfo = 4, AppModel = 5,
        //Equipment = 6, TestData = 7, DBOwner = 8, ConfigSpecs = 9, ChipInfor = 10
        ProductionATS = 1, TestData = 2, MSA = 3, GlobalType = 4, AppModel = 5,
        GlobalEquipment = 6, GlobalSpecs = 7, MCoefGroup = 8, ChipInfo = 9, Management = 10,
        ReportHeader = 11
    }

    public enum CheckAccess
    {
        ViewATSPlan = 0, MofifyATSPlan = 1, AddATSPlan = 2,DeleteATSPlan = 3,
        ViewTestData = 4, 
        ViewMSA = 5, MofifyMSA = 6,
        ViewGlobalType = 7, MofifyGlobalType = 8,
        ViewAppModel = 9, MofifyAppModel = 10,
        ViewEquipment = 11, MofifyEquipment = 12,
        ViewGlobalSpecs = 13, MofifyGlobalSpecs = 14,
        ViewMCoefInfo = 15, MofifyMCoefInfo = 16,
        ViewChipInfor = 17, MofifyChipInfor = 18,
        DBAccessCtrl = 19,
        ViewReportHeader = 20, MofifyReportHeader = 21
    }
    /// <summary>
    /// 确认登入的权限来显示读/编/增/删
    /// </summary>
    /// <param name="funcName">web界面显示的功能列表</param>
    /// <param name="pCheckAccess">各个功能的读/编/增/删权限</param>
    /// <param name="accessCode">用户的权限码</param>
    /// <returns>true 表示有权限,否则无此权限</returns>
    public bool CheckLoginAccess(FunctionName funcName, CheckAccess pCheckAccess, int accessCode)
    {
        bool checkResult = false;
        if (funcName == FunctionName.Management)
        {
            if (pCheckAccess == CheckAccess.DBAccessCtrl)
            {
                checkResult = (accessCode & 0x01) == 80000 ? true : false;
            }
        }
        else if (funcName == FunctionName.ProductionATS)
        {
            if (pCheckAccess == CheckAccess.ViewATSPlan)
            {
                checkResult = (accessCode & 0x01) == 0x01 ? true : false;
            }
            else if (pCheckAccess == CheckAccess.MofifyATSPlan)
            {
                checkResult = (accessCode & 0x02) == 0x02 ? true : false;
            }
            else if (pCheckAccess == CheckAccess.AddATSPlan)
            {
                checkResult = (accessCode & 0x04) == 0x04 ? true : false;
            }
            else if (pCheckAccess == CheckAccess.DeleteATSPlan)
            {
                checkResult = (accessCode & 0x08) == 0x08 ? true : false;
            }
        }
        else if (funcName == FunctionName.TestData)
        {
            if (pCheckAccess == CheckAccess.ViewTestData)
            {
                checkResult = (accessCode & 0x10) == 0x10 ? true : false;
            }
        }
        else if (funcName == FunctionName.MSA)
        {
            if (pCheckAccess == CheckAccess.ViewMSA)
            {
                checkResult = (accessCode & 0x20) == 0x20 ? true : false;
            }
            else if (pCheckAccess == CheckAccess.MofifyMSA)
            {
                checkResult = (accessCode & 0x40) == 0x40 ? true : false;
            }
        }
        else if (funcName == FunctionName.GlobalType)
        {
            if (pCheckAccess == CheckAccess.ViewGlobalType)
            {
                checkResult = (accessCode & 0x80) == 0x80 ? true : false;
            }
            else if (pCheckAccess == CheckAccess.MofifyGlobalType)
            {
                checkResult = (accessCode & 0x100) == 0x100 ? true : false;
            }
        }
        else if (funcName == FunctionName.AppModel)
        {
            if (pCheckAccess == CheckAccess.ViewAppModel)
            {
                checkResult = (accessCode & 0x200) == 0x200 ? true : false;
            }
            else if (pCheckAccess == CheckAccess.MofifyAppModel)
            {
                checkResult = (accessCode & 0x400) == 0x400 ? true : false;
            }
        }
        else if (funcName == FunctionName.GlobalEquipment)
        {
            if (pCheckAccess == CheckAccess.ViewEquipment)
            {
                checkResult = (accessCode & 0x800) == 0x800 ? true : false;
            }
            else if (pCheckAccess == CheckAccess.MofifyEquipment)
            {
                checkResult = (accessCode & 0x1000) == 0x1000 ? true : false;
            }
        }
        else if (funcName == FunctionName.GlobalSpecs)
        {
            if (pCheckAccess == CheckAccess.ViewGlobalSpecs)
            {
                checkResult = (accessCode & 0x2000) == 0x2000 ? true : false;
            }
            if (pCheckAccess == CheckAccess.MofifyGlobalSpecs)
            {
                checkResult = (accessCode & 0x4000) == 0x4000 ? true : false;
            }
        }
        else if (funcName == FunctionName.MCoefGroup)
        {
            if (pCheckAccess == CheckAccess.ViewMCoefInfo)
            {
                checkResult = (accessCode & 0x8000) == 0x8000 ? true : false;
            }
            if (pCheckAccess == CheckAccess.MofifyMCoefInfo)
            {
                checkResult = (accessCode & 0x10000) == 0x10000 ? true : false;
            }
        }
        else if (funcName == FunctionName.ChipInfo)
        {
            if (pCheckAccess == CheckAccess.ViewChipInfor)
            {
                checkResult = (accessCode & 0x20000) == 0x20000 ? true : false;
            }

            if (pCheckAccess == CheckAccess.MofifyChipInfor)
            {
                checkResult = (accessCode & 0x40000) == 0x40000 ? true : false;
            }
        }
        else if (funcName == FunctionName.ReportHeader)
        {
            if (pCheckAccess == CheckAccess.ViewReportHeader)
            {
                checkResult = (accessCode & 0x100000) == 0x100000 ? true : false;
            }

            if (pCheckAccess == CheckAccess.MofifyReportHeader)
            {
                checkResult = (accessCode & 0x200000) == 0x200000 ? true : false;
            }
        }

        return checkResult;
    }
    
}