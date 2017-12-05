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

public partial class _Home : BasePage
{
    DataIO pDataIO;
    string currID = "";
    long myAccessCode =0;
    public DataTable Typedt = new DataTable();
    public DataTable PNdt = new DataTable();
    public DataTable Plandt = new DataTable();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();

        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = "";
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();

        if (Session["DB"] == null)
        {
            Response.Redirect("~/Default.aspx", true);
        }
        else if (Session["DB"].ToString().ToUpper() == "ATSDB")
        {
            dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        }
        else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
        {
            dbName = ConfigurationManager.AppSettings["DbName2"].ToString();
        }

        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
 
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }
        if (myAccessCode == 85004561 && Request.Url.ToString().Contains("UserRoleFunc"))
        {
            Response.Redirect("~/Home.aspx");             
        }
        else
        {
            initPage();
        }
    }

    bool initPage()
    {
        try
        {
            CommCtrl pCtrl = new CommCtrl();
            Table pTable = new Table();
            //OptionButtons1.Visible = false;
            if (Session["UserName"] != null && Session["UserID"] != null && Session["AccCode"] != null)
            {
                UserAccountInfo.hlkUserTxt = Session["UserName"].ToString();
                UserAccountInfo.confighlkUserIDDIV = Session["UserName"].ToString();
            }
            else
            {
                //Response.Write("<script>alert('未找到当前用户!系统自动关闭!')</script>");
                //Response.Write("<script>window.opener = null;window.open('','_self');window.close();</script>");
                //return false;
            }


            if (pDataIO.OpenDatabase(true))
            {
                if (!IsPostBack)
                {
                    DataTable funcBlockInfoDt = pDataIO.GetDataTable("select * from FunctionTable where BlockLevel =0 order by BlockTypeID", "FunctionTable");
                    
                    for (int num = 0; num < funcBlockInfoDt.Rows.Count; num++)
                    {
                        if ((myAccessCode & 0x01) > 0 && funcBlockInfoDt.Rows[num]["ItemName"].ToString().ToUpper() == "产品与测试".ToUpper())
                        {
                            ProductionATSMenu.Attributes.Add("style", "position:relative;display:block;");
#region  ProductionATSMenu
                            Typedt = pDataIO.GetDataTable("select * from GlobalProductionType where IgnoreFlag='false'", "GlobalProductionType");
                            context.Attributes.Add("src", "WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + Typedt.Rows[0]["ID"]);
                            TreeViewMenu1.Nodes.Clear();

                            for (int i = 0; i < Typedt.Rows.Count; i++)
                            {
                                TreeNode typeNode = new TreeNode(Typedt.Rows[i]["ItemName"].ToString());
                                TreeViewMenu1.Nodes.Add(typeNode);                           //添加TypeNode    
                                typeNode.NavigateUrl = "~/WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + Typedt.Rows[i]["ID"];
                                typeNode.Target = "contextName";
                                typeNode.Value = "Type*" + Typedt.Rows[i]["ID"].ToString();
                                typeNode.ToolTip = Typedt.Rows[i]["ItemName"].ToString();
                                if (i == 0)
                                {
                                    typeNode.Selected = true;
                                }

                                int typeid = Convert.ToInt32(Typedt.Rows[i]["ID"]);
                                PNdt = pDataIO.GetDataTable("select * from GlobalProductionName where IgnoreFlag='false' and PID=" + typeid, "GlobalProductionName");
                                //PNdt = pDataIO.GetDataTable("select * from GlobalProductionName where PID=" + typeid, "GlobalProductionName");
                                if (PNdt.Rows.Count != 0)
                                {
                                    TreeNode blankNode = new TreeNode("blank");
                                    typeNode.ChildNodes.Add(blankNode);
                                }
                            }

                            if (Session["TreeNodeExpand"] != null)
                            {
                                if (Session["TreeNodeExpand"].ToString().Contains('+'))
                                {
                                    string[] Index = Session["TreeNodeExpand"].ToString().Split('+');
                                    int length = Index.Length;

                                    if (length == 2)   //新增、修改PN
                                    {
                                        int index1 = Convert.ToInt32(Index[0]);
                                        int index2 = Convert.ToInt32(Index[1]);

                                        TreeViewMenu1.Nodes[index1].Expand();
                                        TreeViewMenu1.Nodes[index1].ChildNodes[index2].Expand();                                  
                                        TreeViewMenu1.Nodes[index1].ChildNodes[index2].ChildNodes[0].Select();
                                    }
                                    else if (length == 3)        //删除Plan
                                    {
                                        int index1 = Convert.ToInt32(Index[0]);
                                        int index2 = Convert.ToInt32(Index[1]);
                                        int index3 = Convert.ToInt32(Index[2]);

                                        TreeViewMenu1.Nodes[index1].Expand();
                                        TreeViewMenu1.Nodes[index1].ChildNodes[index2].Expand();
                                        TreeViewMenu1.Nodes[index1].ChildNodes[index2].ChildNodes[index3].Expand();
                                        TreeViewMenu1.Nodes[index1].ChildNodes[index2].ChildNodes[index3].Select();
                                    }
                                    else if (length == 4)        //新增、修改Plan
                                    {
                                        int index1 = Convert.ToInt32(Index[0]);
                                        int index2 = Convert.ToInt32(Index[1]);
                                        int index3 = Convert.ToInt32(Index[2]);
                                        int index4 = Convert.ToInt32(Index[3]);

                                        TreeViewMenu1.Nodes[index1].Expand();
                                        TreeViewMenu1.Nodes[index1].ChildNodes[index2].Expand();
                                        TreeViewMenu1.Nodes[index1].ChildNodes[index2].ChildNodes[index3].Expand();
                                        TreeViewMenu1.Nodes[index1].ChildNodes[index2].ChildNodes[index3].ChildNodes[index4].Select();
                                    }
                                }
                                else     //删除PN、新增、修改Type
                                {
                                    int index1 = Convert.ToInt32(Session["TreeNodeExpand"].ToString());
                                    TreeViewMenu1.Nodes[index1].Expand();
                                    TreeViewMenu1.Nodes[index1].Select();
                                }

                                context.Attributes.Add("src", Session["iframe_src"].ToString());
                            }
                            #endregion
                        }
                        else if ((myAccessCode & 0x10) > 0 && funcBlockInfoDt.Rows[num]["ItemName"].ToString().ToUpper() == "测试数据".ToUpper())
                        {
                            TestDataMenu.Attributes.Add("style", "position:relative;display:block;");
                        }
                        else if ((myAccessCode & 0x80000) > 0 && funcBlockInfoDt.Rows[num]["ItemName"].ToString().ToUpper() == "管理".ToUpper())
                        {
                            ManagementMenu.Attributes.Add("style", "position:relative;display:block;");
                        }
                    }

#region  MaintainMenu
                    if ((myAccessCode & 0x20) > 0 || (myAccessCode & 0x80) > 0 || (myAccessCode & 0x200) > 0 || (myAccessCode & 0x800) > 0 || (myAccessCode & 0x2000) > 0 || (myAccessCode & 0x8000) > 0 || (myAccessCode & 0x2000) > 0)
                    {
                        MaintainMenu.Attributes.Add("style", "position:relative;display:block;");
                        TreeViewMenu3.Nodes.Clear();

                        if ((myAccessCode & 0x20) > 0)        //有读取MSA信息权限
                        {
                            TreeNode MSANode = new TreeNode("MSA");
                            TreeViewMenu3.Nodes.Add(MSANode);                            
                            MSANode.NavigateUrl = "~/WebFiles/MaintainInfo/MSAInfo/MSAModuleTypeList.aspx";
                            MSANode.Target = "contextName";
                            MSANode.Value = "MSA";
                        }

                        if ((myAccessCode & 0x80) > 0)        //有读取产品类型信息权限
                        {
                            TreeNode ProductionTypeNode = new TreeNode("产品类型");
                            TreeViewMenu3.Nodes.Add(ProductionTypeNode);
                            ProductionTypeNode.NavigateUrl = "~/WebFiles/MaintainInfo/ProductionType/ProductionModuleTypeList.aspx";
                            ProductionTypeNode.Target = "contextName";
                            ProductionTypeNode.Value = "ProductionType";
                            ProductionTypeNode.ToolTip = "ProductionType";
                        }

                        if ((myAccessCode & 0x200) > 0)        //有读取GlobalModel信息权限
                        {
                            TreeNode AppModelNode = new TreeNode("测试模型");
                            TreeViewMenu3.Nodes.Add(AppModelNode);
                            AppModelNode.NavigateUrl = "~/WebFiles/MaintainInfo/AppModel/GlobalModelList.aspx";
                            AppModelNode.Target = "contextName";
                            AppModelNode.Value = "AppModel";
                            AppModelNode.ToolTip = "AppModel";
                        }

                        if ((myAccessCode & 0x800) > 0)        //有读取设备信息权限
                        {
                            TreeNode EquipmentNode = new TreeNode("设备列表");
                            TreeViewMenu3.Nodes.Add(EquipmentNode);
                            EquipmentNode.NavigateUrl = "~/WebFiles/MaintainInfo/Equipment/GlobalEquipList.aspx";
                            EquipmentNode.Target = "contextName";
                            EquipmentNode.Value = "Equipment";
                            EquipmentNode.ToolTip = "Equipment";
                        }
                        
                        if ((myAccessCode & 0x2000) > 0)        //有读取公共产品参数配置权限
                        {
                            TreeNode GlobalSpecsNode = new TreeNode("规格参数");
                            TreeViewMenu3.Nodes.Add(GlobalSpecsNode);
                            GlobalSpecsNode.NavigateUrl = "~/WebFiles/MaintainInfo/GlobalSpecs/GlobalSpecsList.aspx";
                            GlobalSpecsNode.Target = "contextName";
                            GlobalSpecsNode.Value = "GlobalSpecs";
                            GlobalSpecsNode.ToolTip = "GlobalSpecs";
                        }
                        
                        if ((myAccessCode & 0x8000) > 0)        //有读取MCoef信息权限
                        {
                            TreeNode MCoefGroupNode = new TreeNode("模块系数");
                            TreeViewMenu3.Nodes.Add(MCoefGroupNode);
                            MCoefGroupNode.NavigateUrl = "~/WebFiles/MaintainInfo/MCoefGroup/MCoefGroupType.aspx";
                            MCoefGroupNode.Target = "contextName";
                            MCoefGroupNode.Value = "MCoefGroup";
                            MCoefGroupNode.ToolTip = "MCoefGroup";
                        }
                        
                        if ((myAccessCode & 0x20000) > 0)        //有读取读取芯片信息权限
                        {
                            TreeNode ChipInfoNode = new TreeNode("芯片信息");
                            TreeViewMenu3.Nodes.Add(ChipInfoNode);
                            ChipInfoNode.NavigateUrl = "~/WebFiles/MaintainInfo/Chip/ChipBaseList.aspx";
                            ChipInfoNode.Target = "contextName";
                            ChipInfoNode.Value = "ChipInfo";
                            ChipInfoNode.ToolTip = "ChipInfo";
                        }

                        if ((myAccessCode & 0x100000) > 0)        //有读取读取报表表头权限
                        {
                            TreeNode ReportHeaderNode = new TreeNode("报表表头");
                            TreeViewMenu3.Nodes.Add(ReportHeaderNode);
                            ReportHeaderNode.NavigateUrl = "~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderList.aspx";
                            ReportHeaderNode.Target = "contextName";
                            ReportHeaderNode.Value = "ReportHeader";
                            ReportHeaderNode.ToolTip = "ReportHeader";
                        }
                    }
                    #endregion

                    LabelRoot.Text = "Copyright &copy; ATS信息管理系统 V2.0 &nbsp;&lt;UpdateTime" + ConfigurationManager.AppSettings["UpdataTime"].ToString() + "&gt;" + "&nbsp;——";
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        LabelRootDB.Text = "&nbsp; Database：ATS_V2";
                        LabelRootDB.ForeColor = System.Drawing.Color.Black;
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        LabelRootDB.Text = "&nbsp; Database：ATS_VXDebug";
                        LabelRootDB.ForeColor = System.Drawing.Color.Red;
                    }
                   
                }
            }
            return true;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void TreeViewMenu1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {      
        if (e.Node.Value.Contains("Type"))
        {
            string TypeId = e.Node.Value.Split('*')[1];
            //PNdt = pDataIO.GetDataTable("select * from GlobalProductionName where PID=" + TypeId, "GlobalProductionName");
            PNdt = pDataIO.GetDataTable("select * from GlobalProductionName where IgnoreFlag='false' and PID=" + TypeId + " order by wsFlag", "GlobalProductionName");
           
            e.Node.ChildNodes.Clear();
            for (int j = 0; j < PNdt.Rows.Count; j++)
            {
                TreeNode PNNode = new TreeNode();
                if (PNdt.Rows[j]["wsFlag"].ToString().ToUpper().Trim() == "TRUE")
                {
                    PNNode.Text = "<font color='Gray'>" + PNdt.Rows[j]["PN"].ToString() + "(WS)" + "</font>";
                }
                else
                {
                    PNNode.Text = PNdt.Rows[j]["PN"].ToString();
                }

                e.Node.ChildNodes.Add(PNNode);                          //添加PNNode
                PNNode.NavigateUrl = "~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + PNdt.Rows[j]["ID"];
                PNNode.Target = "contextName";
                PNNode.Value = "PN*" + PNdt.Rows[j]["ID"].ToString();
                PNNode.ToolTip = PNdt.Rows[j]["PN"].ToString();

                TreeNode blankNode = new TreeNode("blank");
                PNNode.ChildNodes.Add(blankNode);  
            }
        }
        else if (e.Node.Value.Contains("PN"))
        {
            e.Node.ChildNodes.Clear();
            string PNId = e.Node.Value.Split('*')[1];
            TreeNode PNInfoNode = new TreeNode("产品信息");
            e.Node.ChildNodes.Add(PNInfoNode);                           //添加PNInfoNode
            PNInfoNode.NavigateUrl = "~/WebFiles/Production_ATS/Production/PNSelfInfor.aspx?AddNew=false&uId=" + PNId;
            PNInfoNode.Target = "contextName";
            PNInfoNode.Value = "ProductionInfo*" + PNId;
            PNInfoNode.ToolTip = "PNInfo";
            TreeNode blankNode = new TreeNode("blank");
            PNInfoNode.ChildNodes.Add(blankNode);

            TreeNode TestPlanNode = new TreeNode("测试方案");
            e.Node.ChildNodes.Add(TestPlanNode);                           //添加TestPlanNode
            TestPlanNode.NavigateUrl = "~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + PNId;
            TestPlanNode.Target = "contextName";
            TestPlanNode.Value = "TestPlan*" + PNId;
            TestPlanNode.ToolTip = "TestPlan";
            Plandt = pDataIO.GetDataTable("select * from TopoTestPlan where IgnoreFlag='false' and PID=" + PNId, "TopoTestPlan");
            if (Plandt.Rows.Count != 0)
            {
                TreeNode blankNode2 = new TreeNode("blank");
                TestPlanNode.ChildNodes.Add(blankNode2);
            }

            TreeNode EEPROMNode = new TreeNode("EEPROM");               //添加EEPROMNode
            e.Node.ChildNodes.Add(EEPROMNode);
            EEPROMNode.NavigateUrl = "~/WebFiles/Production_ATS/Production/TopEEROMList.aspx?AddNew=false&uId=" + PNId;
            EEPROMNode.Target = "contextName";       
        }
        else if (e.Node.Value.Contains("ProductionInfo"))
        {
            if (e.Node.ChildNodes.Count != 3)
            {
                e.Node.ChildNodes.Clear();
                string PNId = e.Node.Value.Split('*')[1];
                TreeNode ChipsetControlNode = new TreeNode("芯片控制");        //添加ChipsetControl、Chip、ChipsetInitNode
                TreeNode ChipNode = new TreeNode("芯片信息");
                TreeNode PNChipsetInitNode = new TreeNode("芯片初始化");
                e.Node.ChildNodes.Add(ChipsetControlNode);
                e.Node.ChildNodes.Add(ChipNode);
                e.Node.ChildNodes.Add(PNChipsetInitNode);
                ChipsetControlNode.NavigateUrl = "~/WebFiles/Production_ATS/Production/ChipSetControlList.aspx?AddNew=false&uId=" + PNId;
                ChipsetControlNode.Target = "contextName";
                ChipsetControlNode.ToolTip = "ChipsetControl";
                ChipNode.NavigateUrl = "~/WebFiles/Production_ATS/Production/PNChipList.aspx?AddNew=false&uId=" + PNId;
                ChipNode.Target = "contextName";
                ChipNode.ToolTip = " Chip";
                PNChipsetInitNode.NavigateUrl = "~/WebFiles/Production_ATS/Production/ChipSetIniList.aspx?AddNew=false&uId=" + PNId;
                PNChipsetInitNode.Target = "contextName";
                PNChipsetInitNode.ToolTip = "PNChipsetInit";
            }           
        }
        else if (e.Node.Value.Contains("TestPlan"))
        {
            e.Node.ChildNodes.Clear();
            string PNId = e.Node.Value.Split('*')[1];
            Plandt = pDataIO.GetDataTable("select * from TopoTestPlan where IgnoreFlag='false' and PID=" + PNId + " order by ItemName", "TopoTestPlan");
            for (int m = 0; m < Plandt.Rows.Count; m++)
            {
                TreeNode PlanNameNode = new TreeNode(Plandt.Rows[m]["ItemName"].ToString());
                e.Node.ChildNodes.Add(PlanNameNode);                    //添加PlanNameNode
                PlanNameNode.NavigateUrl = "~/WebFiles/Production_ATS/TestPlan/TestplanSelfInfor.aspx?uId=" + Plandt.Rows[m]["ID"];
                PlanNameNode.Target = "contextName";
                PlanNameNode.Value = "PlanName*" + Plandt.Rows[m]["ID"];
                PlanNameNode.ToolTip = Plandt.Rows[m]["ItemName"].ToString();

                TreeNode blankNode = new TreeNode("PlanName");
                PlanNameNode.ChildNodes.Add(blankNode);
            }
        }
        else if (e.Node.Value.Contains("PlanName"))
        {
            if (e.Node.ChildNodes.Count != 5)
            {
                e.Node.ChildNodes.Clear();
                string PlanId = e.Node.Value.Split('*')[1];
                TreeNode FlowControlNode = new TreeNode("流程控制");      //添加FlowControl、Equipment、Specs、MconfigInit、ChipsetInitNode
                TreeNode EquipmentNode = new TreeNode("设备列表");
                TreeNode SpecsNode = new TreeNode("规格参数");
                TreeNode MconfigInitNode = new TreeNode("模块配置");
                TreeNode ChipsetInitNode = new TreeNode("芯片初始化");
                e.Node.ChildNodes.Add(FlowControlNode);
                e.Node.ChildNodes.Add(EquipmentNode);
                e.Node.ChildNodes.Add(SpecsNode);
                e.Node.ChildNodes.Add(MconfigInitNode);
                e.Node.ChildNodes.Add(ChipsetInitNode);
                FlowControlNode.NavigateUrl = "~/WebFiles/Production_ATS/TestPlan/FlowControlList.aspx?uId=" + PlanId;
                FlowControlNode.Target = "contextName";
                FlowControlNode.ToolTip = "FlowControl";
                EquipmentNode.NavigateUrl = "~/WebFiles/Production_ATS/TestPlan/EquipmentList.aspx?uId=" + PlanId;
                EquipmentNode.Target = "contextName";
                EquipmentNode.ToolTip = "Equipment";
                SpecsNode.NavigateUrl = "~/WebFiles/Production_ATS/TestPlan/TopPNSpecList.aspx?uId=" + PlanId;
                SpecsNode.Target = "contextName";
                SpecsNode.ToolTip = "Specs";
                MconfigInitNode.NavigateUrl = "~/WebFiles/Production_ATS/TestPlan/ManufaConfigInit.aspx?uId=" + PlanId;
                MconfigInitNode.Target = "contextName";
                MconfigInitNode.ToolTip = "MconfigInit";
                ChipsetInitNode.NavigateUrl = "~/WebFiles/Production_ATS/TestPlan/ChipSetIniList.aspx?AddNew=false&uId=" + PlanId;
                ChipsetInitNode.Target = "contextName";
                ChipsetInitNode.ToolTip = "PlanChipsetInit";
            }
        }
    }
}