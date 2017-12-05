<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="TestControll.aspx.cs" Inherits="WebFiles_TestReport_TestControll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
    <table>
    <tr>
    <td></td>
    </tr>
    </table>
    </div>
    
    <div>
        <table>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="SN:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Label" Width="148px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="TestPlan:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Label" Width="148px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="StartTime:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Label" Width="148px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td><asp:Button ID="Button1" runat="server" Text="返回" Width="87px" 
                        onclick="Button1_Click" Enabled="False" Visible="False" /></td>
                        <td>&nbsp; &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            </table>
    </div>
    
<div>
<table>
            <tr>
                <td>&nbsp;</td>
                <td>
 <asp:GridView ID="GridView1" class="Grid" runat="server" Width="507px" AllowPaging="True" 
            pagesize="20" onpageindexchanging="GridView1_PageIndexChanging" EnableViewState="False" 
                        onrowcommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" 
                        AutoGenerateColumns="False">
            <Columns>
            <asp:BoundField DataField="Temp" HeaderText="Temp" />
            <asp:BoundField DataField="Vcc" HeaderText="Vcc" />
            <asp:BoundField DataField="Channel" HeaderText="Channel" />
            <asp:BoundField DataField="CtrlType" HeaderText="CtrlType" />
            <asp:BoundField DataField="Result" HeaderText="Result" />
            <asp:TemplateField HeaderText="FMTData" >
            <ItemTemplate>
            <asp:LinkButton  ID="TestDataLinkButton"  runat="server"  CommandName="TestData">View
            </asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="LPData" >
            <ItemTemplate>
            <asp:LinkButton  ID="LPDataLinkButton"  runat="server"  CommandName="LPData">View
            </asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="ID" >
                    <ItemStyle  CssClass="hide" BorderColor="#507CD1" />
                    <HeaderStyle CssClass="hide" />
                </asp:BoundField>
            </Columns>
            
            <HeaderStyle BackColor="#0099CC" Font-Bold="True" ForeColor="White" CssClass="gridviewtop" Wrap="false"/>
        </asp:GridView>
        </td>
            </tr>
        </table>
</div>

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="OptionContent" Runat="Server">
</asp:Content>

