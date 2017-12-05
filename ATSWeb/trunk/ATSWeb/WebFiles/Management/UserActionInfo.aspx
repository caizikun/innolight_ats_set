<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="UserActionInfo.aspx.cs" Inherits="WebFiles_UserRoleFunc_UserActionInfo" %>

<%@ Register src="~/Frame/OptionButtons.ascx" tagname="OptionButtons" tagprefix="uc1" %>
<%@ Register src="~/Frame/UserRoleFunc/PNActionList.ascx" tagname="PNActionList" tagprefix="uc2" %>
<%--<%@ Register src="../../Frame/UserRoleFunc/PlanActionList.ascx" tagname="PlanActionList" tagprefix="uc3" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Headaspx" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/App_Themes/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.11.2.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/RefreshTime.js") %>"></script>  
</head>

<body onkeydown="if (event.keyCode == 116){window.location.reload();return false;}">
 <form id="formaspx" runat="server">
 <div id="Navi" class="Navi">
  <div class="NaviList" >
         <asp:PlaceHolder ID="plhNavi"  Runat="Server" ></asp:PlaceHolder> 
</div>
</div> 

<div id="Options" class="Options">
 <div class="OptionLog" >
     <asp:Image ID="ImageList" runat="server" ImageUrl="~/Images/selfInfo.gif" 
         Height="20px" />                      
 </div>
 <div class="OptionLabel" >              
      <asp:Label ID="LabelList" runat="server" Text="用户权限信息"></asp:Label>
 </div>
  <div class="OptionButton">                            
       <uc1:OptionButtons ID="OptionButtons1" runat="server" />                           
 </div> 
 </div>

<div id="Main" class="Main">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>

        <style type="text/css">
        .style1
        {
            height: 20px;
        }
    
        .style2
        {
            height: 15px;
        }
        
        .style3
        {
            width: 52px;
        }
        </style>

        <div>    
        <table>
            <tr> <td height="2px"></td></tr>
            <tr>
               <td> 
                   <asp:Label ID="Label1" runat="server" Text="[产品类型]" Font-Bold="True"></asp:Label>
                </td>
            </tr>

            <tr>
               <td class="style1"> 
                   <asp:DropDownList ID="DropDownListType" runat="server" Height="22px" 
                       Width="144px"  AutoPostBack="True" 
                       onselectedindexchanged="DropDownListType_SelectedIndexChanged">
                   </asp:DropDownList>
                </td>
            </tr>

            <tr>
               <td class="style2" > </td>
            </tr>

            <tr>
               <td class="style1"> 
                   <asp:Label ID="Label2"  Width="160px"  runat="server" Text="[PN权限]" Font-Bold="True"></asp:Label>
                </td>
            </tr>

        </table>

        <table  style="margin-left: 550px; margin-top: -24px;" >
           <tr>
             <td>
                <asp:Label ID="Label3" runat="server" Text="[TestPlan权限]" Font-Bold="True"></asp:Label>
             </td>
           </tr>
        </table>

        
        <table>
          <tr>   
            <td width="390px"   style="vertical-align:top "> <asp:PlaceHolder ID="plhMain" runat="server"></asp:PlaceHolder></td>           
            <td class="style3"></td>       
            <td style="vertical-align:top">
             <asp:GridView ID="GridView1" class="Grid" runat="server" Width="500px"
                        onrowcreated="GridView1_RowCreated" 
                        AutoGenerateColumns="False" >
            
            <Columns>
            <%-- <asp:BoundField DataField="PNID" HeaderText="PNID"/>
            <asp:BoundField DataField="PlanID" HeaderText="PlanID"  /> --%>
                             
            <asp:TemplateField HeaderText="PNID" Visible="False">
            <ItemTemplate>
                <asp:Label ID="PNIDLabel" runat="server" Text="Label"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>    
                 
            <asp:TemplateField HeaderText="PlanID" Visible="False">
            <ItemTemplate>
                <asp:Label ID="PlanIDLabel" runat="server" Text="Label"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField> 
              
             <asp:BoundField DataField="TestPlan" HeaderText="测试方案" />   
                     
            <asp:TemplateField HeaderText="可编辑?">
            <ItemTemplate>
               <asp:CheckBox ID="CheckBoxEdit" runat="server" Enabled="False" /> 
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="可删除?" >
            <ItemTemplate>
               <asp:CheckBox ID="CheckBoxDelete" runat="server" Enabled="False" /> 
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="可运行?" >
            <ItemTemplate>
               <asp:CheckBox ID="CheckBoxRun" runat="server" Enabled="False" /> 
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="ActionID" Visible="False">
            <ItemTemplate>
                <asp:Label ID="ActionIDLabel" runat="server" Text="Label"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField> 

            </Columns>
                 <EmptyDataTemplate >
                       <%--<table>--%>
                       <%-- <tr>--%>   
                           <th width="260px"  height="23px" ID="TH4Title" class="tHStyleCenter1" runat="server">
                               <asp:Label ID="TH4" runat="server" Text="测试方案"></asp:Label>
                           </th>
                           <th width="80px" height="23px" ID="TH1Title" class="tHStyleCenter1" runat="server">
                               <asp:Label ID="TH1" runat="server" Text="可编辑?"></asp:Label>
                           </th>
                           <th width="80px" height="23px" ID="TH2Title" class="tHStyleCenter1" runat="server">
                               <asp:Label ID="TH2" runat="server" Text="可删除?"></asp:Label>
                           </th>
                            <th width="80px" height="23px" ID="TH3Title" class="tHStyleCenter1" runat="server">
                               <asp:Label ID="TH3" runat="server" Text="可运行?"></asp:Label>
                           </th>

                       <%-- </tr>  --%>
                       <%-- </table>--%>
                 </EmptyDataTemplate>

            <HeaderStyle BackColor="#87CEFA" Font-Bold="True" ForeColor="White" CssClass="tHStyleCenter1" Height="29px"  Wrap="false"/>
            <RowStyle Height="29px"/>
        </asp:GridView></td>
          </tr>
        </table>
            
           

        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

    <script type="text/javascript" language="JavaScript">
        var winHeight = 558;
        function findDimensions() {                  //函数：获取尺寸
            if (document.documentElement && document.documentElement.clientHeight) {
                winHeight = document.documentElement.clientHeight - 60;
            }
            if (document.getElementById("Main")) {
                document.getElementById("Main").style.height = winHeight + "px";
            }
        }
        findDimensions();
        window.onresize = findDimensions;

   </script> 
 </form>
</body>
</html>
