<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="OPLogs.aspx.cs" Inherits="WebFiles_OperateLog_OPLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
    <style type="text/css">
        .style2
        {
            width: 83px;
        }
        .style3
        {
            width: 92px;
        }
        .style4
        {
            width: 35px;
        }
        .style5
        {
            width: 160px;
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
                <td>&nbsp;</td>
                <td class="style2">
                    <asp:Label ID="Label1" runat="server" Text="UserName:"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="Label7" runat="server" Text="Label" Width="123px" 
                        Font-Underline="True"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="Label2" runat="server" Text="LogInTime:"></asp:Label>
                </td>
                <td class="style1">
                    <asp:Label ID="Label8" runat="server" Text="Label" Width="260px" 
                        Font-Underline="True" style="margin-left: 0px"></asp:Label>
                </td>
                <td class="style4">
                    <asp:Label ID="Label3" runat="server" Text="IP:"></asp:Label>
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
                        pagesize="20" onpageindexchanging="GridView1_PageIndexChanging" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            onselectedindexchanging="GridView1_SelectedIndexChanging" 
                        onrowcommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" 
                        AutoGenerateColumns="False" >
            
            <Columns>
            
            <asp:BoundField DataField="ModifyTime" HeaderText="ModifyTime"   DataFormatString="{0:yyyy/MM/dd HH:mm:ss.fff}" HtmlEncode="false"  SortExpression="DateTime"/>
            <asp:BoundField DataField="OpType" HeaderText="OpType" />
            <asp:BoundField DataField="BlockType" HeaderText="BlockType" />
            <asp:BoundField DataField="TracingInfo" HeaderText="TracingInfo" />
            <%-- 
            <asp:BoundField DataField="OperateItem" HeaderText="OperateItem" />
            <asp:BoundField DataField="ChlidItem" HeaderText="ChlidItem" />--%>
            <asp:TemplateField HeaderText="DetailLogs" >
            <ItemTemplate>
            <asp:LinkButton  ID="DetailLogsLinkButton"  runat="server"  CommandName="DetailLogs" Visible="True" Enabled="False">仅登录查看，未进行任何修改操作
            </asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            
            </Columns>
            <HeaderStyle BackColor="#0099CC" Font-Bold="True" ForeColor="White" CssClass="gridviewtop" Wrap="false"/>
        </asp:GridView></td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
            </tr>
        </table>
</div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="OptionContent" Runat="Server">
</asp:Content>

