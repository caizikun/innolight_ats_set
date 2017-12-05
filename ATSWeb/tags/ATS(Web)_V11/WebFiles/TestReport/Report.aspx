<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="WebFiles_TestReport_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style2
        {
            width: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ModulesNameListContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviContent" Runat="Server">
    <table border="0" cellspacing="0" cellpadding="0" class="NaviLeftMargin" >
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
    <td>&nbsp;</td>
    <td>
        <asp:Label ID="Label4" runat="server" Text="Type:"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
            Height="21px" ontextchanged="DropDownList1_TextChanged" Width="155px">
        </asp:DropDownList>
    </td>
    <td class="style2">&nbsp;&nbsp;</td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="PN:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                        Height="21px" ontextchanged="DropDownList2_TextChanged" Width="155px">
                    </asp:DropDownList>
                </td>
                <td class="style2">&nbsp;</td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="TestPlan:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList3" runat="server" Height="21px" Width="155px">
                    </asp:DropDownList>
                </td>
    </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="SN:"></asp:Label>
                </td>
                <td><asp:TextBox ID="TextBox1" runat="server" Width="151px"></asp:TextBox>
                </td>
                <td class="style2">&nbsp;</td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="StartTime:"></asp:Label>
                </td>
                <td><asp:TextBox ID="TextBox5" runat="server" Width="151px" 
                        ToolTip="2015/1/1 00:00:00"></asp:TextBox>
                </td>
                <td class="style2">&nbsp;</td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="StopTime:"></asp:Label>
                </td>
                <td><asp:TextBox ID="TextBox6" runat="server" Width="151px" 
                        ToolTip="2015/1/1 23:59:59"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td><asp:Button ID="Button1" runat="server" Text="查看报表" Width="87px" 
                        onclick="Button1_Click" style="height: 21px" /></td>
                        <td>&nbsp; </td>
                <td><asp:Button ID="Button2" runat="server" Text="导出数据" Width="87px" 
                        onclick="Button2_Click" style="height: 21px" /></td>
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
 <asp:GridView ID="GridView1" class="Grid" runat="server" Width="238px" AllowPaging="True" 
            pagesize="20" onpageindexchanging="GridView1_PageIndexChanging" EnableViewState="False">
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="gridviewtop" Wrap="false"/>
        </asp:GridView>
        </td>
            </tr>
        </table>
</div>
<div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="OptionContent" Runat="Server">
</asp:Content>

