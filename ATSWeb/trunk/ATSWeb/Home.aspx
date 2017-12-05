<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Home.aspx.cs" Inherits="_Home" %>

<%@ Register src="Frame/AccountInfo.ascx" tagname="AccountInfo" tagprefix="uc1" %>
<%@ Register src="Frame/OptionButtons.ascx" tagname="OptionButtons" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/App_Themes/Site.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.11.2.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/RefreshTime.js") %>"></script>   
</head>
<body  onload="startTime()" scroll="no" onkeydown="reloadIframe();return false;">
 <form id="Form1" defaultbutton="" runat="server" scroll="no">
 <div   id="primary"class="show" scroll="no">
    <div id="div_head" class="div_head">
        <div>          
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logo.png" />
        </div>               
        <div id="UserInfor">
           <uc1:AccountInfo ID="UserAccountInfo" runat="server" />      
        </div>               
    </div>
    <div id="waitImg" class="wait" style="position:absolute; top:95px; height:20px; width:100px; right:10px;display:block;"></div>
<div id="Middle">
</div>
    <div id="ModulesNameList">
        <asp:ScriptManager ID="ScriptManagerTree" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePaneltree" runat="server">
         <ContentTemplate>
    <div id="ProductionATSMenu" runat="server" style="position:relative;display:none;"> 
        <asp:ImageButton ID="Menu1" runat="server" ImageUrl="~/Images/Menu.png" 
            onClientClick="Menu1Click();return false;" />
        <asp:Label ID="LbMenu1" runat="server" Text="产品与测试" ForeColor="white" style=" position:absolute; left:15px;top:7px; 
            font-size: 18px; font-weight: 700;"></asp:Label>
        <asp:ImageButton ID="ImMenu1Unfold" runat="server" ImageUrl="~/Images/unfold.png" onClientClick="ImMenu1UnfoldClick();return false;" style=" position:absolute;left:248px; top:6px; display:block;"/>
        <asp:ImageButton ID="ImMenu1Fold" runat="server" ImageUrl="~/Images/fold.png" onClientClick="ImMenu1FoldClick();return false;" style=" position:absolute;left:248px; top:6px; display:none;"/>
       <asp:TreeView ID="TreeViewMenu1" runat="server" 
            style="display:block;font-family :微软雅黑;" ForeColor="black" Font-Size="14px"
            ShowLines="True" ExpandDepth="0" SkipLinkText="" 
            ontreenodeexpanded="TreeViewMenu1_TreeNodeExpanded" >
           <NodeStyle ForeColor="Black" Font-Size="14px" />
          <%-- <SelectedNodeStyle ForeColor="#D90000" Font-Bold="True" Font-Size="15px" />--%>
       </asp:TreeView>
    </div>
      
    <div id="TestDataMenu" runat="server" style="position:relative;display:none;">
        <asp:ImageButton ID="Menu2" runat="server" ImageUrl="~/Images/Menu.png" 
            onClientClick="Menu2Click();return false;" />
        <asp:Label ID="LbMenu2" runat="server" Text="测试数据" ForeColor="white"  style=" position:absolute; left:15px;top:7px; 
            font-size: 18px; font-weight: 700;" ></asp:Label>
        <asp:ImageButton ID="ImMenu2Unfold" runat="server" ImageUrl="~/Images/unfold.png" onClientClick="ImMenu2UnfoldClick();return false;" style=" position:absolute;left:248px; top:6px; display:none;"/>
        <asp:ImageButton ID="ImMenu2Fold" runat="server" ImageUrl="~/Images/fold.png" onClientClick="ImMenu2FoldClick();return false;" style=" position:absolute;left:248px; top:6px; display:block;"/>
       <asp:TreeView ID="TreeViewMenu2" runat="server" style=" display:none;font-family :微软雅黑;" ForeColor="black" Font-Size="14px" 
            ShowLines="True"  ExpandDepth="0" SkipLinkText="">
            <Nodes>
                <asp:TreeNode Text="查询" Value="Query" NavigateUrl="WebFiles/TestReport/Query.aspx" Target="contextName"></asp:TreeNode>
                <asp:TreeNode Text="报表" Value="Report" NavigateUrl="WebFiles/TestReport/Report.aspx" Target="contextName"></asp:TreeNode>
            </Nodes>
            <NodeStyle ForeColor="Black" Font-Size="14px" />
           <%--<SelectedNodeStyle ForeColor="#D90000" Font-Bold="True" Font-Size="15px" />--%>
       </asp:TreeView>
    </div>

    <div id="MaintainMenu" runat="server" style="position:relative;display:none;">
        <asp:ImageButton ID="Menu3" runat="server" ImageUrl="~/Images/Menu.png" 
            onClientClick="Menu3Click();return false;"/>
        <asp:Label ID="LbMenu3" runat="server" Text="维护" ForeColor="white"  style=" position:absolute; left:15px;top:7px; 
            font-size: 18px; font-weight: 700;" ></asp:Label>
        <asp:ImageButton ID="ImMenu3Unfold" runat="server" ImageUrl="~/Images/unfold.png" onClientClick="ImMenu3UnfoldClick();return false;" style=" position:absolute;left:248px; top:6px; display:none;"/>
        <asp:ImageButton ID="ImMenu3Fold" runat="server" ImageUrl="~/Images/fold.png" onClientClick="ImMenu3FoldClick();return false;" style=" position:absolute;left:248px; top:6px; display:block;"/>
        <asp:TreeView ID="TreeViewMenu3" runat="server" style=" display:none;font-family :微软雅黑;" ForeColor="black" Font-Size="14px" 
            ShowLines="True"  ExpandDepth="0" SkipLinkText="">
            <NodeStyle ForeColor="Black" Font-Size="14px" />
           <%--<SelectedNodeStyle ForeColor="#D90000" Font-Bold="True" Font-Size="15px" />--%>
       </asp:TreeView>
    </div>
       
    <div id="ManagementMenu" runat="server" style="position:relative;display:none;">
        <asp:ImageButton ID="Menu4" runat="server" ImageUrl="~/Images/Menu.png" 
            onClientClick="Menu4Click();return false;" />          
        <asp:Label ID="LbMenu4" runat="server" Text="管理" ForeColor="white"  style=" position:absolute; left:15px;top:7px;
            font-size: 18px; font-weight: 700;" ></asp:Label>
        <asp:ImageButton ID="ImMenu4Unfold" runat="server" ImageUrl="~/Images/unfold.png" onClientClick="ImMenu4UnfoldClick();return false;" style=" position:absolute;left:248px; top:6px; display:none;"/>
        <asp:ImageButton ID="ImMenu4Fold" runat="server" ImageUrl="~/Images/fold.png" onClientClick="ImMenu4FoldClick();return false;" style=" position:absolute;left:248px; top:6px; display:block;"/>
        <asp:TreeView ID="TreeViewMenu4" runat="server" style=" display:none; font-family :微软雅黑;" ForeColor="black" Font-Size="14px" 
            ShowLines="True" ExpandDepth="0" SkipLinkText="">
            <Nodes>
                <asp:TreeNode Text="用户列表" Value="AccountList" NavigateUrl="WebFiles/Management/UserList.aspx" Target="contextName"></asp:TreeNode>
                <asp:TreeNode Text="角色列表" Value="RoleList" NavigateUrl="WebFiles/Management/RoleList.aspx" Target="contextName"></asp:TreeNode>                
                <asp:TreeNode Text="功能块列表" Value="FunctionList" NavigateUrl="WebFiles/Management/FuncList.aspx" Target="contextName"></asp:TreeNode>
                <asp:TreeNode Text="用户操作日志" Value="UserOPLogs" NavigateUrl="WebFiles/Management/UserOpInfo.aspx" Target="contextName"></asp:TreeNode>                
            </Nodes>
            <NodeStyle ForeColor="Black" Font-Size="14px" />
           <%--<SelectedNodeStyle ForeColor="#D90000" Font-Bold="True" Font-Size="15px" />--%>
       </asp:TreeView>
    </div>

        <div id="HelpMenu" runat="server" style="position:relative;">
        <asp:ImageButton ID="Menu5" runat="server" ImageUrl="~/Images/Menu.png" 
            onClientClick="Menu5Click();return false;" />          
        <asp:Label ID="LbMenu5" runat="server" Text="帮助" ForeColor="white"  style=" position:absolute; left:15px;top:7px;
            font-size: 18px; font-weight: 700;" ></asp:Label>
        <asp:ImageButton ID="ImMenu5Unfold" runat="server" ImageUrl="~/Images/unfold.png" onClientClick="ImMenu5UnfoldClick();return false;" style=" position:absolute;left:248px; top:6px; display:none;"/>
        <asp:ImageButton ID="ImMenu5Fold" runat="server" ImageUrl="~/Images/fold.png" onClientClick="ImMenu5FoldClick();return false;" style=" position:absolute;left:248px; top:6px; display:block;"/>
        <asp:TreeView ID="TreeViewMenu5" runat="server" style=" display:none; font-family :微软雅黑;" ForeColor="black" Font-Size="14px" 
            ShowLines="True" ExpandDepth="0" SkipLinkText="">
            <Nodes>
                <asp:TreeNode Text="产品与测试" Value="Help1" Expanded="true" SelectAction="None">
                     <asp:TreeNode Text="产品信息" Value="ProductionHelp" Expanded="true" SelectAction="None">
                         <asp:TreeNode Text="PN信息" Value="PNHelp" NavigateUrl="WebFiles/Help/PNHelp.htm" Target="contextName"></asp:TreeNode>                
                         <asp:TreeNode Text="芯片控制信息" Value="ChipsetControlHelp" NavigateUrl="WebFiles/Help/ChipsetControlHelp.htm" Target="contextName"></asp:TreeNode>
                         <asp:TreeNode Text="芯片初始化信息" Value="PNChipsetInitHelp" NavigateUrl="WebFiles/Help/PNChipsetInitHelp.htm" Target="contextName"></asp:TreeNode>       
                         <asp:TreeNode Text="新建PN" Value="AddPNHelp" NavigateUrl="WebFiles/Help/AddPNHelp.htm" Target="contextName"></asp:TreeNode>                         
                     </asp:TreeNode>  
                     <asp:TreeNode Text="测试方案" Value="TestplanHelp" Expanded="true" SelectAction="None">
                         <asp:TreeNode Text="TestPlan信息" Value="TestPlanHelp" NavigateUrl="WebFiles/Help/TestPlanHelp.htm" Target="contextName"></asp:TreeNode>                
                         <asp:TreeNode Text="流程控制信息" Value="FlowControlHelp" NavigateUrl="WebFiles/Help/FlowControlHelp.htm" Target="contextName"></asp:TreeNode>
                         <asp:TreeNode Text="测试模型信息" Value="TestModelHelp" NavigateUrl="WebFiles/Help/TestModelHelp.htm" Target="contextName"></asp:TreeNode>       
                         <asp:TreeNode Text="设备列表信息" Value="EquipmentHelp" NavigateUrl="WebFiles/Help/EquipmentHelp.htm" Target="contextName"></asp:TreeNode> 
                         <asp:TreeNode Text="规格参数信息" Value="SpecHelp" NavigateUrl="WebFiles/Help/SpecHelp.htm" Target="contextName"></asp:TreeNode> 
                         <asp:TreeNode Text="模块配置信息" Value="MConfigHelp" NavigateUrl="WebFiles/Help/MConfigHelp.htm" Target="contextName"></asp:TreeNode>
                         <asp:TreeNode Text="芯片初始化信息" Value="PlanChipsetInitHelp" NavigateUrl="WebFiles/Help/PlanChipsetInitHelp.htm" Target="contextName"></asp:TreeNode>       
                         <asp:TreeNode Text="新建Testplan" Value="AddPlanHelp" NavigateUrl="WebFiles/Help/AddPlanHelp.htm" Target="contextName"></asp:TreeNode>                                                 
                     </asp:TreeNode>                                   
                </asp:TreeNode>             
            </Nodes>
            <NodeStyle ForeColor="Black" Font-Size="14px" />
           <%--<SelectedNodeStyle ForeColor="#D90000" Font-Bold="True" Font-Size="15px" />--%>
       </asp:TreeView>
    </div>

       </ContentTemplate>
      </asp:UpdatePanel>
      
        </div>  

  <iframe id="context" name="contextName" class="context" frameborder="0" 
         scrolling="auto" runat="server"></iframe>

 
    <div id="Root">      
        <asp:Label ID="LabelRoot" runat="server" Text=""  style=" line-height :25px;"> 
         </asp:Label>  
        <asp:Label ID="LabelRootDB" runat="server"  style=" line-height :25px;" 
            Font-Bold="True" ForeColor="Red" ></asp:Label>
    </div>
         
    <script type="text/javascript" language="JavaScript">
              var winWidth = 1170;
              var winHeight = 558;
              var winHeightMain = 498;
              function findDimensions() { //函数：获取尺寸
                  //通过深入Document内部对body进行检测，获取窗口大小
                  if (document.documentElement && document.documentElement.clientHeight && document.documentElement.clientWidth) {
                      winHeight = document.documentElement.clientHeight - 115;
                      winHeightMain = document.documentElement.clientHeight - 175;
                      winWidth = document.body.clientWidth - 290;
                  }
                  //设置div的具体宽度=窗口的宽度的%
                  if (document.getElementById("Middle")) {
                      document.getElementById("Middle").style.height = winHeight + "px";
                  }
                  if (document.getElementById("ModulesNameList")) {
                      document.getElementById("ModulesNameList").style.height = winHeight + "px";
                  }
                  if (document.getElementById("context")) {
                      document.getElementById("context").style.height = winHeight + "px";
                      document.getElementById("context").style.width = winWidth + "px";
                  }
              }
              findDimensions();
              window.onresize = findDimensions;

              function Menu1Click() {
                  if (document.getElementById("TreeViewMenu1").style.display == "block") {
                      document.getElementById("TreeViewMenu1").style.display = "none";
                      document.getElementById("ImMenu1Unfold").style.display = "none";
                      document.getElementById("ImMenu1Fold").style.display = "block";
                  }
                  else {
                      document.getElementById("TreeViewMenu1").style.display = "block";
                      document.getElementById("ImMenu1Unfold").style.display = "block";
                      document.getElementById("ImMenu1Fold").style.display = "none";

                      document.getElementById("TreeViewMenu2").style.display = "none";
                      document.getElementById("ImMenu2Unfold").style.display = "none";
                      document.getElementById("ImMenu2Fold").style.display = "block";

                      document.getElementById("TreeViewMenu3").style.display = "none";
                      document.getElementById("ImMenu3Unfold").style.display = "none";
                      document.getElementById("ImMenu3Fold").style.display = "block";

                      document.getElementById("TreeViewMenu4").style.display = "none";
                      document.getElementById("ImMenu4Unfold").style.display = "none";
                      document.getElementById("ImMenu4Fold").style.display = "block";

                      document.getElementById("TreeViewMenu5").style.display = "none";
                      document.getElementById("ImMenu5Unfold").style.display = "none";
                      document.getElementById("ImMenu5Fold").style.display = "block";                
                  }
              }

              function ImMenu1UnfoldClick() {
                  document.getElementById("TreeViewMenu1").style.display = "none";
                  document.getElementById("ImMenu1Unfold").style.display = "none";
                  document.getElementById("ImMenu1Fold").style.display = "block";
               }

               function ImMenu1FoldClick() {
                   document.getElementById("TreeViewMenu1").style.display = "block";
                   document.getElementById("ImMenu1Unfold").style.display = "block";
                   document.getElementById("ImMenu1Fold").style.display = "none";

                   document.getElementById("TreeViewMenu2").style.display = "none";
                   document.getElementById("ImMenu2Unfold").style.display = "none";
                   document.getElementById("ImMenu2Fold").style.display = "block";

                   document.getElementById("TreeViewMenu3").style.display = "none";
                   document.getElementById("ImMenu3Unfold").style.display = "none";
                   document.getElementById("ImMenu3Fold").style.display = "block";

                   document.getElementById("TreeViewMenu4").style.display = "none";
                   document.getElementById("ImMenu4Unfold").style.display = "none";
                   document.getElementById("ImMenu4Fold").style.display = "block";

                   document.getElementById("TreeViewMenu5").style.display = "none";
                   document.getElementById("ImMenu5Unfold").style.display = "none";
                   document.getElementById("ImMenu5Fold").style.display = "block";    
               }

               function Menu2Click() {
                   if (document.getElementById("TreeViewMenu2").style.display == "block") {
                       document.getElementById("TreeViewMenu2").style.display = "none";
                       document.getElementById("ImMenu2Unfold").style.display = "none";
                       document.getElementById("ImMenu2Fold").style.display = "block";
                   }
                   else {
                       document.getElementById("TreeViewMenu2").style.display = "block";
                       document.getElementById("ImMenu2Unfold").style.display = "block";
                       document.getElementById("ImMenu2Fold").style.display = "none";

                       document.getElementById("TreeViewMenu1").style.display = "none";
                       document.getElementById("ImMenu1Unfold").style.display = "none";
                       document.getElementById("ImMenu1Fold").style.display = "block";

                       document.getElementById("TreeViewMenu3").style.display = "none";
                       document.getElementById("ImMenu3Unfold").style.display = "none";
                       document.getElementById("ImMenu3Fold").style.display = "block";

                       document.getElementById("TreeViewMenu4").style.display = "none";
                       document.getElementById("ImMenu4Unfold").style.display = "none";
                       document.getElementById("ImMenu4Fold").style.display = "block";

                       document.getElementById("TreeViewMenu5").style.display = "none";
                       document.getElementById("ImMenu5Unfold").style.display = "none";
                       document.getElementById("ImMenu5Fold").style.display = "block";   
                   } 
              }

              function Menu3Click() {
                  if (document.getElementById("TreeViewMenu3").style.display == "block") {
                      document.getElementById("TreeViewMenu3").style.display = "none";
                      document.getElementById("ImMenu3Unfold").style.display = "none";
                      document.getElementById("ImMenu3Fold").style.display = "block";
                  }
                  else {
                      document.getElementById("TreeViewMenu3").style.display = "block";
                      document.getElementById("ImMenu3Unfold").style.display = "block";
                      document.getElementById("ImMenu3Fold").style.display = "none";

                      document.getElementById("TreeViewMenu1").style.display = "none";
                      document.getElementById("ImMenu1Unfold").style.display = "none";
                      document.getElementById("ImMenu1Fold").style.display = "block";

                      document.getElementById("TreeViewMenu2").style.display = "none";
                      document.getElementById("ImMenu2Unfold").style.display = "none";
                      document.getElementById("ImMenu2Fold").style.display = "block";

                      document.getElementById("TreeViewMenu4").style.display = "none";
                      document.getElementById("ImMenu4Unfold").style.display = "none";
                      document.getElementById("ImMenu4Fold").style.display = "block";

                      document.getElementById("TreeViewMenu5").style.display = "none";
                      document.getElementById("ImMenu5Unfold").style.display = "none";
                      document.getElementById("ImMenu5Fold").style.display = "block";   
                  }
              }

              function Menu4Click() {
                  if (document.getElementById("TreeViewMenu4").style.display == "block") {
                      document.getElementById("TreeViewMenu4").style.display = "none";
                      document.getElementById("ImMenu4Unfold").style.display = "none";
                      document.getElementById("ImMenu4Fold").style.display = "block";
                  }
                  else {
                      document.getElementById("TreeViewMenu4").style.display = "block";
                      document.getElementById("ImMenu4Unfold").style.display = "block";
                      document.getElementById("ImMenu4Fold").style.display = "none";

                      document.getElementById("TreeViewMenu1").style.display = "none";
                      document.getElementById("ImMenu1Unfold").style.display = "none";
                      document.getElementById("ImMenu1Fold").style.display = "block";

                      document.getElementById("TreeViewMenu2").style.display = "none";
                      document.getElementById("ImMenu2Unfold").style.display = "none";
                      document.getElementById("ImMenu2Fold").style.display = "block";

                      document.getElementById("TreeViewMenu3").style.display = "none";
                      document.getElementById("ImMenu3Unfold").style.display = "none";
                      document.getElementById("ImMenu3Fold").style.display = "block";

                      document.getElementById("TreeViewMenu5").style.display = "none";
                      document.getElementById("ImMenu5Unfold").style.display = "none";
                      document.getElementById("ImMenu5Fold").style.display = "block";   
                  }
              }

              function Menu5Click() {
                  if (document.getElementById("TreeViewMenu5").style.display == "block") {
                      document.getElementById("TreeViewMenu5").style.display = "none";
                      document.getElementById("ImMenu5Unfold").style.display = "none";
                      document.getElementById("ImMenu5Fold").style.display = "block";
                  }
                  else {
                      document.getElementById("TreeViewMenu5").style.display = "block";
                      document.getElementById("ImMenu5Unfold").style.display = "block";
                      document.getElementById("ImMenu5Fold").style.display = "none";

                      document.getElementById("TreeViewMenu1").style.display = "none";
                      document.getElementById("ImMenu1Unfold").style.display = "none";
                      document.getElementById("ImMenu1Fold").style.display = "block";

                      document.getElementById("TreeViewMenu2").style.display = "none";
                      document.getElementById("ImMenu2Unfold").style.display = "none";
                      document.getElementById("ImMenu2Fold").style.display = "block";

                      document.getElementById("TreeViewMenu3").style.display = "none";
                      document.getElementById("ImMenu3Unfold").style.display = "none";
                      document.getElementById("ImMenu3Fold").style.display = "block";

                      document.getElementById("TreeViewMenu4").style.display = "none";
                      document.getElementById("ImMenu4Unfold").style.display = "none";
                      document.getElementById("ImMenu4Fold").style.display = "block";
                  }
              }

              function ImMenu2UnfoldClick() {
                  document.getElementById("TreeViewMenu2").style.display = "none";
                  document.getElementById("ImMenu2Unfold").style.display = "none";
                  document.getElementById("ImMenu2Fold").style.display = "block";
              }

              function ImMenu2FoldClick() {
                  document.getElementById("TreeViewMenu2").style.display = "block";
                  document.getElementById("ImMenu2Unfold").style.display = "block";
                  document.getElementById("ImMenu2Fold").style.display = "none";

                  document.getElementById("TreeViewMenu1").style.display = "none";
                  document.getElementById("ImMenu1Unfold").style.display = "none";
                  document.getElementById("ImMenu1Fold").style.display = "block";

                  document.getElementById("TreeViewMenu3").style.display = "none";
                  document.getElementById("ImMenu3Unfold").style.display = "none";
                  document.getElementById("ImMenu3Fold").style.display = "block";

                  document.getElementById("TreeViewMenu4").style.display = "none";
                  document.getElementById("ImMenu4Unfold").style.display = "none";
                  document.getElementById("ImMenu4Fold").style.display = "block";

                  document.getElementById("TreeViewMenu5").style.display = "none";
                  document.getElementById("ImMenu5Unfold").style.display = "none";
                  document.getElementById("ImMenu5Fold").style.display = "block";
              }

              function ImMenu3UnfoldClick() {
                  document.getElementById("TreeViewMenu3").style.display = "none";
                  document.getElementById("ImMenu3Unfold").style.display = "none";
                  document.getElementById("ImMenu3Fold").style.display = "block";
              }

              function ImMenu3FoldClick() {
                  document.getElementById("TreeViewMenu3").style.display = "block";
                  document.getElementById("ImMenu3Unfold").style.display = "block";
                  document.getElementById("ImMenu3Fold").style.display = "none";

                  document.getElementById("TreeViewMenu2").style.display = "none";
                  document.getElementById("ImMenu2Unfold").style.display = "none";
                  document.getElementById("ImMenu2Fold").style.display = "block";

                  document.getElementById("TreeViewMenu1").style.display = "none";
                  document.getElementById("ImMenu1Unfold").style.display = "none";
                  document.getElementById("ImMenu1Fold").style.display = "block";

                  document.getElementById("TreeViewMenu4").style.display = "none";
                  document.getElementById("ImMenu4Unfold").style.display = "none";
                  document.getElementById("ImMenu4Fold").style.display = "block";

                  document.getElementById("TreeViewMenu5").style.display = "none";
                  document.getElementById("ImMenu5Unfold").style.display = "none";
                  document.getElementById("ImMenu5Fold").style.display = "block";
              }

              function ImMenu4UnfoldClick() {
                  document.getElementById("TreeViewMenu4").style.display = "none";
                  document.getElementById("ImMenu4Unfold").style.display = "none";
                  document.getElementById("ImMenu4Fold").style.display = "block";
              }

              function ImMenu4FoldClick() {
                  document.getElementById("TreeViewMenu4").style.display = "block";
                  document.getElementById("ImMenu4Unfold").style.display = "block";
                  document.getElementById("ImMenu4Fold").style.display = "none";

                  document.getElementById("TreeViewMenu2").style.display = "none";
                  document.getElementById("ImMenu2Unfold").style.display = "none";
                  document.getElementById("ImMenu2Fold").style.display = "block";

                  document.getElementById("TreeViewMenu3").style.display = "none";
                  document.getElementById("ImMenu3Unfold").style.display = "none";
                  document.getElementById("ImMenu3Fold").style.display = "block";

                  document.getElementById("TreeViewMenu1").style.display = "none";
                  document.getElementById("ImMenu1Unfold").style.display = "none";
                  document.getElementById("ImMenu1Fold").style.display = "block";

                  document.getElementById("TreeViewMenu5").style.display = "none";
                  document.getElementById("ImMenu5Unfold").style.display = "none";
                  document.getElementById("ImMenu5Fold").style.display = "block";
              }

              function ImMenu5UnfoldClick() {
                  document.getElementById("TreeViewMenu5").style.display = "none";
                  document.getElementById("ImMenu5Unfold").style.display = "none";
                  document.getElementById("ImMenu5Fold").style.display = "block";
              }

              function ImMenu5FoldClick() {
                  document.getElementById("TreeViewMenu5").style.display = "block";
                  document.getElementById("ImMenu5Unfold").style.display = "block";
                  document.getElementById("ImMenu5Fold").style.display = "none";

                  document.getElementById("TreeViewMenu2").style.display = "none";
                  document.getElementById("ImMenu2Unfold").style.display = "none";
                  document.getElementById("ImMenu2Fold").style.display = "block";

                  document.getElementById("TreeViewMenu3").style.display = "none";
                  document.getElementById("ImMenu3Unfold").style.display = "none";
                  document.getElementById("ImMenu3Fold").style.display = "block";

                  document.getElementById("TreeViewMenu1").style.display = "none";
                  document.getElementById("ImMenu1Unfold").style.display = "none";
                  document.getElementById("ImMenu1Fold").style.display = "block";

                  document.getElementById("TreeViewMenu4").style.display = "none";
                  document.getElementById("ImMenu4Unfold").style.display = "none";
                  document.getElementById("ImMenu4Fold").style.display = "block";
              }

              function RefreshTreeNode() {
                  window.location.reload();
                  //                  window.top.location.reload();         
              }

              document.getElementById("context").onload = function () {
                  document.getElementById("waitImg").style.display = "none";
              } 
   </script> 


 </div>
    </form>
</body>
</html>