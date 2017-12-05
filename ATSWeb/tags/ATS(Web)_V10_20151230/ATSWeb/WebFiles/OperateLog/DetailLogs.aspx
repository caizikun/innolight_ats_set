<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="DetailLogs.aspx.cs" Inherits="WebFiles_OperateLog_DetailLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ModulesNameListContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviContent" Runat="Server">
 <table border="0" cellspacing="0" cellpadding="0"  class="NaviLeftMargin">
	<tr class="tdCenter" >
		<td id="tdVCenter">
         <asp:PlaceHolder ID="plhNavi"  Runat="Server" ></asp:PlaceHolder> 
        </td>
	</tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="UserInforContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
<div>
    <table style="width: 1076px">
        <tr>
            <td></td>
        </tr>
        <tr>
                <td ></td>
                <td class="style2">
                    <asp:Label ID="Label1" runat="server" Text="UserName:"></asp:Label>
                </td>
                <td class="style1">
                    <asp:Label ID="Label7" runat="server" Text="Label" Width="92px" 
                        Font-Underline="True"></asp:Label>
                </td>
                
                <td class="style3">
                    <asp:Label ID="Label2" runat="server" Text="ModifyTime:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Label" Width="260px" 
                        Font-Underline="True"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="IP:"></asp:Label>
                    <asp:Label ID="Label9" runat="server" Text="Label" Width="260px" 
                        Font-Underline="True" style="margin-left: 12px"></asp:Label>
                </td>
                
                
        </tr>

        <tr>
        <td>
        </td>
        <td class="style2">
                <asp:Label ID="Label4" runat="server" Text="OpType:"></asp:Label>
            </td>
                    <td class="style1"><asp:Label ID="Label10" runat="server" Text="Label" Width="92px" 
                    Font-Underline="True"></asp:Label>
            </td>
            <td class="style3">
                <asp:Label ID="Label5" runat="server" Text="BlockType:"></asp:Label>
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
    
    <asp:Label ID="Label6" runat="server" Text="OperateItem:" Style="margin-left: 8px"></asp:Label>
    <asp:Label ID="Label12" runat="server" Text="Label" Width="972px" 
        Font-Underline="True" style="margin-top: 3px"></asp:Label>
</div>
    
<div>
<table>
            <tr>
                <td>&nbsp;</td>
               
       
        <td>
        <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Height="481px" 
                Width="1010px" style="margin-top: 8px; line-height:30px;"></asp:TextBox>
        </td>
            </tr>
        </table>
</div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="OptionContent" Runat="Server">
</asp:Content>

