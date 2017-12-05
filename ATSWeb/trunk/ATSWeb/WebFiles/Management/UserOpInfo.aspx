<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="UserOpInfo.aspx.cs" Inherits="WebFiles_OperateLog_UserOpInfo" %>

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
    .style4
    {
        width: 10px;
    }    
    </style>
</head>

<body onkeydown="if (event.keyCode == 116){window.location.reload();return false;}">
 <form id="formaspx" runat="server">
 <div id="waitImg" class="wait" style="position:absolute; top:5px; height:20px; width:100px; right:10px;display:none;"></div>
 <div id="Navi" class="Navi">
 <div class="NaviList" >
         <asp:PlaceHolder ID="plhNavi"  Runat="Server" onprerender="plhNavi_PreRender" ></asp:PlaceHolder> 
 </div>
 </div>

<div id="Main" class="Main">
<div>
<table>
<tr>
<td class="style4"></td>
</tr>
<tr style=" height:30px;">
<td></td>
<td>

                <asp:Panel ID="Panel1" runat="server" Height="25px" Width="965px" 
                    DefaultButton="Button1">
                    <asp:Label ID="Label1" runat="server" Text="用户名：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="148px" height="24px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Label ID="Label5" runat="server" Text="起始时间：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
                    <asp:TextBox ID="TextBox5" runat="server" ToolTip="2015/1/1 00:00:00" 
                        Width="148px" height="18px"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Label ID="Label6" runat="server" Text="截止时间：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
                    <asp:TextBox ID="TextBox6" runat="server" ToolTip="2015/1/1 23:59:59" 
                        Width="148px" height="18px"></asp:TextBox>
                        &nbsp;&nbsp; &nbsp;
                        <asp:Button ID="Button1" runat="server" Text="" Height="28px" Width="90px" class="btQuery" 
                        onclick="Button1_Click" UseSubmitBehavior="False" />
                </asp:Panel>

            </td>   
</tr>
</table>
</div>
<div>
        <table>
            <tr>
                <td>&nbsp;&nbsp;</td>
                
                <td>
        <asp:GridView ID="GridView1" class="Grid" runat="server" Width="737px" AllowPaging="True" 
                        pagesize="15" onpageindexchanging="GridView1_PageIndexChanging" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            onselectedindexchanging="GridView1_SelectedIndexChanging" 
                        onrowcommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" 
                        AutoGenerateColumns="False"  CellPadding="4">
            
            <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID"/> <%--获取ID 但是不显示--%>
            <asp:BoundField DataField="UserName" HeaderText="用户名" />
            <asp:BoundField DataField="LogInTime" HeaderText="登录时间" DataFormatString="{0:yyyy/MM/dd HH:mm:ss.fff}" HtmlEncode="false"  SortExpression="DateTime"/>
            
            <%-- <asp:BoundField DataField="LogOffTime" HeaderText="LogOffTime" DataFormatString="{0:yyyy/MM/dd HH:mm:ss.fff}" HtmlEncode="false"  SortExpression="DateTime"/>
            --%>
            <asp:BoundField DataField="LoginInfo" HeaderText="电脑IP" />
            <asp:TemplateField HeaderText="操作内容" ItemStyle-Width="106px">
            <ItemTemplate>
            <asp:LinkButton  ID="OpLogsLinkButton"  runat="server"  CommandName="OpLogs" Width="106px"  style="text-align:center;">查看
            </asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            
            </Columns>
            <HeaderStyle CssClass="tHStyleCenter" Wrap="false"/>
            <RowStyle Height="29px"/>
        </asp:GridView>
        </td>
            </tr>
        </table>
    </div>
    
    <div>
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

         //         window.onload = function () {
         //             document.getElementById("waitImg").style.display = "none";
         //         }
   </script> 
 </form>
</body>
</html>

