<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="DetailLogs.aspx.cs" Inherits="WebFiles_OperateLog_DetailLogs" %>

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
            width: 74px;
        }
        .style2
        {
            width: 86px;
        }
        .style3
        {
            width: 99px;
        }
    </style>
</head>

<body onkeydown="if (event.keyCode == 116){window.location.reload();return false;}">
 <form id="formaspx" runat="server">
 <div id="waitImg" class="wait" style="position:absolute; top:5px; height:20px; width:100px; right:10px;display:none;"></div>
 <div id="Navi" class="Navi">
 <div class="NaviList" >
         <asp:PlaceHolder ID="plhNavi"  Runat="Server" ></asp:PlaceHolder> 
 </div>
 </div>

<div id="Main" class="Main">
<div>
    <table style="width: 1015px">
        <tr>
            <td style="width: 15px"></td>
        </tr>
        <tr>
                <td ></td>
                <td class="style2">
                    <asp:Label ID="Label1" runat="server" Text="用户名:"></asp:Label>
                </td>
                <td class="style1">
                    <asp:Label ID="Label7" runat="server" Text="Label" Width="92px" 
                        Font-Underline="True"></asp:Label>
                </td>
                
                <td class="style3">
                    <asp:Label ID="Label2" runat="server" Text="操作时间:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Label" Width="260px" 
                        Font-Underline="True"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="电脑IP:"></asp:Label>
                    <asp:Label ID="Label9" runat="server" Text="Label" Width="260px" 
                        Font-Underline="True" style="margin-left: 12px"></asp:Label>
                </td>
                
                
        </tr>

        <tr>
        <td>
        </td>
        <td class="style2">
                <asp:Label ID="Label4" runat="server" Text="类型:"></asp:Label>
            </td>
                    <td class="style1"><asp:Label ID="Label10" runat="server" Text="Label" Width="92px" 
                    Font-Underline="True"></asp:Label>
            </td>
            <td class="style3">
                <asp:Label ID="Label5" runat="server" Text="功能块:"></asp:Label>
            </td>
            <td>

                <asp:Label ID="Label11" runat="server" Text="Label" Width="260px" 
                    Font-Underline="True"></asp:Label>
            </td>
            <td>          
                <%-- 150529      
                <asp:Label ID="Label13" runat="server" Text="ChlidItem:"></asp:Label>
                --%>
            </td>
        </tr>
            
    </table>
    
    <asp:Label ID="Label6" runat="server" Text="操作信息:" Style="margin-left: 21px"></asp:Label>
    <asp:Label ID="Label12" runat="server" Text="Label" Width="972px" 
        Font-Underline="True" style="margin-top: 3px"></asp:Label>
</div>
    
<div>
<table>
            <tr>
                <td>&nbsp;</td>
               
       
        <td>
        <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Height="430px" 
                Width="1010px" style="margin-top: 8px; line-height:30px;"></asp:TextBox>
        </td>
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

