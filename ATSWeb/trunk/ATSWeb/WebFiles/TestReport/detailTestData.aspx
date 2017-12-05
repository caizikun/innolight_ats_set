<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="detailTestData.aspx.cs" Inherits="WebFiles_TestReport_AdvancedSelect" %>

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

<div id="Main" class="Main"> 
    <div style=" height:10px;">
    </div>
    <div>
        <table>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="SN："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Label" Width="148px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="开始时间："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Label" Width="148px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="测试方案："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Label" Width="400px" 
                        Font-Underline="True"></asp:Label>
                </td>              
            </tr>

            <tr>           
            <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="温度："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Label" Width="148px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="电压："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Label" Width="148px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="通道："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Label" Width="148px" 
                        Font-Underline="True"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    
<div>
<table>
            <tr>
                <td>&nbsp;</td>
                <td>
 <asp:GridView ID="GridView1" class="Grid" runat="server" Width="600px"
            CellPadding="4">
            <HeaderStyle CssClass="tHStyleCenterBottom" Wrap="false"/>
            <RowStyle Height="29px"/>
        </asp:GridView>
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

