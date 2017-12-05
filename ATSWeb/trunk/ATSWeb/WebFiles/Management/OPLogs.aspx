<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="OPLogs.aspx.cs" Inherits="WebFiles_OperateLog_OPLogs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Headaspx" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/App_Themes/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.11.2.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/RefreshTime.js") %>"></script>  

    <style type="text/css">
        .style1
        {
            width: 260px;
        }
        .style2
        {
            width: 68px;
        }
        .style3
        {
            width: 82px;
        }
        .style4
        {
            width: 67px;
        }
        .style5
        {
            width: 160px;
        }
    </style>
</head>
        
<body onkeydown="if (event.keyCode == 116){window.location.reload();return false;}">
 <form id="formaspx" runat="server">
 <div id="Navi" class="Navi">
 <div class="NaviList" >
         <asp:PlaceHolder ID="plhNavi"  Runat="Server" ></asp:PlaceHolder> 
 </div>
 </div>

<div id="Main" class="MainReport">  
<div>
        <table style="width: 1076px">
        <tr>
<td></td>
</tr>
            <tr>
                <td>&nbsp;</td>
                <td class="style2">
                    <asp:Label ID="Label1" runat="server" Text="用户名："></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="Label7" runat="server" Text="Label" Width="123px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="Label2" runat="server" Text="登录时间："></asp:Label>
                </td>
                <td class="style1">
                    <asp:Label ID="Label8" runat="server" Text="Label" Width="200px" 
                        Font-Underline="True" style="margin-left: 0px"></asp:Label>
                </td>
                <td class="style4">
                    <asp:Label ID="Label3" runat="server" Text="电脑IP："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Label" Width="148px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                        <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>

                    &nbsp;</td>
            </tr>

            <tr>
            <td>
            </td>
            </tr>
        </table>
    </div>
    
<div>
<table>
            <tr>
                <td>&nbsp;</td>
               
        <td>
            <asp:GridView ID="GridView1" class="Grid" runat="server" Width="737px" AllowPaging="True" 
                        pagesize="15" onpageindexchanging="GridView1_PageIndexChanging" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            onselectedindexchanging="GridView1_SelectedIndexChanging" 
                        onrowcommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" 
                        AutoGenerateColumns="False" CellPadding="4">
            
            <Columns>
            
            <asp:BoundField DataField="ModifyTime" HeaderText="操作时间"   DataFormatString="{0:yyyy/MM/dd HH:mm:ss.fff}" HtmlEncode="false"  SortExpression="DateTime"/>
            <asp:BoundField DataField="OpType" HeaderText="类型" />
            <asp:BoundField DataField="BlockType" HeaderText="功能块" />
            <asp:BoundField DataField="TracingInfo" HeaderText="追踪信息" />
            <%-- 
            <asp:BoundField DataField="OperateItem" HeaderText="OperateItem" />
            <asp:BoundField DataField="ChlidItem" HeaderText="ChlidItem" />--%>
            <asp:TemplateField HeaderText="操作详情" >
            <ItemTemplate>
            <asp:LinkButton  ID="DetailLogsLinkButton"  runat="server"  CommandName="DetailLogs" Visible="True" Enabled="False">仅登录查看，未进行任何修改操作
            </asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            
            </Columns>
             <HeaderStyle CssClass="tHStyleCenter" Wrap="false"/>
             <RowStyle Height="29px"/>
        </asp:GridView></td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
            </tr>
        </table>
</div>
</div>  

     <script type="text/javascript" language="JavaScript">
         var winHeight = 558;
         function findDimensions() {                  //函数：获取尺寸
             if (document.documentElement && document.documentElement.clientHeight) {
                 winHeight = document.documentElement.clientHeight - 30;
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

