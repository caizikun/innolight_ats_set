<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="BackCoef.aspx.cs" Inherits="WebFiles_TestReport_BackCoef" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style2
        {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ModulesNameListContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviContent" Runat="Server">
<table border="0" cellspacing="0" cellpadding="0"  class="NaviLeftMargin" >
	<tr>
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

    </div>
    <div>
        <table>
            <tr>
                <td class="style2"> &nbsp;</td>
                <td class="style2">
                    <asp:Label ID="Label1" runat="server" Text="SN:"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="Label4" runat="server" Text="Label" Width="148px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td class="style2"></td>
                <td class="style2">
                    <asp:Label ID="Label2" runat="server" Text="TestPlan:"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="Label5" runat="server" Text="Label" Width="148px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td class="style2"></td>
                <td class="style2">
                    <asp:Label ID="Label3" runat="server" Text="StartTime:"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="Label6" runat="server" Text="Label" Width="148px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td class="style2"></td>
                <td class="style2"><asp:Button ID="Button1" runat="server" Text="返回" Width="87px" 
                        onclick="Button1_Click" Enabled="False" Visible="False" /></td>
                        <td class="style2">&nbsp; &nbsp;</td>
                <td class="style2"></td>
            </tr>

        </table>
    </div>

<div>
 <table>
            <tr>
                <td>&nbsp;</td>
                <td>
 <asp:GridView ID="GridView1" class="Grid" runat="server" Width="238px" AllowPaging="True" 
            pagesize="20" onpageindexchanging="GridView1_PageIndexChanging">
            <HeaderStyle BackColor="#0099CC" Font-Bold="True" ForeColor="White"  Wrap="false"/>
        </asp:GridView>
        </td>
            </tr>
        </table>
</div>

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="OptionContent" Runat="Server">
</asp:Content>

