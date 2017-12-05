<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="UserOpInfo.aspx.cs" Inherits="WebFiles_OperateLog_UserOpInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

        .style4
        {
            width: 10px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ModulesNameListContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviContent" Runat="Server">
    <table border="0" cellspacing="0" cellpadding="0"  class="NaviLeftMargin">
	<tr class="tdCenter" >
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
<table>
<tr>
<td class="style4"></td>
</tr>
<tr>
<td></td>
<td>

                <asp:Panel ID="Panel1" runat="server" Height="25px" Width="965px" 
                    DefaultButton="Button1">
                    <asp:Label ID="Label1" runat="server" Text="UserName:"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="148px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Label ID="Label5" runat="server" Text="StartTime:"></asp:Label>
                    <asp:TextBox ID="TextBox5" runat="server" ToolTip="2015/1/1 00:00:00" 
                        Width="148px"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Label ID="Label6" runat="server" Text="StopTime:"></asp:Label>
                    <asp:TextBox ID="TextBox6" runat="server" ToolTip="2015/1/1 23:59:59" 
                        Width="148px"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Text="查询" Width="87px" 
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
                        pagesize="20" onpageindexchanging="GridView1_PageIndexChanging" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            onselectedindexchanging="GridView1_SelectedIndexChanging" 
                        onrowcommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" 
                        AutoGenerateColumns="False" >
            
            <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID"/> <%--获取ID 但是不显示--%>
            <asp:BoundField DataField="UserName" HeaderText="UserName" />
            <asp:BoundField DataField="LogInTime" HeaderText="LogInTime" DataFormatString="{0:yyyy/MM/dd HH:mm:ss.fff}" HtmlEncode="false"  SortExpression="DateTime"/>
            
            <%-- <asp:BoundField DataField="LogOffTime" HeaderText="LogOffTime" DataFormatString="{0:yyyy/MM/dd HH:mm:ss.fff}" HtmlEncode="false"  SortExpression="DateTime"/>
            --%>
            <asp:BoundField DataField="LoginInfo" HeaderText="IP" />
            <asp:TemplateField HeaderText="OpLogs" >
            <ItemTemplate>
            <asp:LinkButton  ID="OpLogsLinkButton"  runat="server"  CommandName="OpLogs">View
            </asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            
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

