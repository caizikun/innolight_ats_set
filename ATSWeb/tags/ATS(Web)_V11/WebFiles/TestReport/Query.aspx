<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Query.aspx.cs" Inherits="WebFiles_TestReport_Query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ModulesNameListContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviContent" Runat="Server">
<table border="0" cellspacing="0" cellpadding="0"  class="NaviLeftMargin" >
	<tr>
		<td id="tdVCenter">
         <asp:PlaceHolder ID="plhNavi"  Runat="Server" onprerender="plhNavi_PreRender" ></asp:PlaceHolder> 
        </td>
	</tr>
</table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="UserInforContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
  <%--  <table>
    <tr>
    <td></td>
    </tr>
    </table>--%>
    </div>
    
    <div>
        <table>
            <tr>
                <td>&nbsp;&nbsp;</td>
                
                <td>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1" Width="660px">
                    <asp:Label ID="Label1" runat="server" Text="SN:"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" Width="614px" Height="19px" ></asp:TextBox>
                    
                </asp:Panel>
                </td>
                <td>
                   <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="查询" 
                          UseSubmitBehavior="False" Width="87px" Height="23px" />
                </td>

            </tr>
<%--            <tr>
            <td>
            </td>
            </tr>--%>
        </table>
    </div>
    
    <div>
        <table style="margin-top: 0px">
            <tr>
                <td class="style2">&nbsp;&nbsp;</td>
                
                <td>
        <asp:GridView ID="GridView1" class="Grid" runat="server" Width="585px" AllowPaging="True" 
                        pagesize="20" onpageindexchanging="GridView1_PageIndexChanging" EnableViewState="False" 
                        onrowcommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" 
                        AutoGenerateColumns="False" >
            
            <Columns>
            <asp:BoundField DataField="SN" HeaderText="SN" />
            <asp:BoundField DataField="TestPlan" HeaderText="TestPlan" />
            <asp:BoundField DataField="StartTime" HeaderText="StartTime" />
            <asp:BoundField DataField="EndTime" HeaderText="EndTime" />
            <asp:BoundField DataField="Result" HeaderText="Result" />
            <asp:TemplateField HeaderText="TestControll" >
            <ItemTemplate>
            <asp:LinkButton  ID="TestControllLinkButton"  runat="server"  CommandName="TestControll" Width="106px">View
            </asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <Columns>
            <asp:TemplateField HeaderText="BackCoef">
            <ItemTemplate>
            <asp:LinkButton  ID="BackCoefLinkButton"  runat="server" CommandName="BackCoef" Width="84px">View
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
    
    <div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="OptionContent" Runat="Server">
  
    
</asp:Content>

