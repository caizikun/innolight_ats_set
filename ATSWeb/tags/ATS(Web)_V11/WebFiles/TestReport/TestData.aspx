<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="TestData.aspx.cs" Inherits="WebFiles_TestReport_TestData" %>

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
<td>

</td>
</tr>
</table>

</div>
    <div>
    <center>
        <table style="width: 100%">
        
            <tr>
                <td>
                <center>
                    <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="欢迎使用查询功能"></asp:Label>
                </center>
                </td>
                
            </tr>
            <tr>
                <td>
                <center>
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Size="Large" 
                        NavigateUrl="~/WebFiles/TestReport/Query.aspx" ToolTip="查询测试数据以及系数配置信息">Query</asp:HyperLink>
                </center>
                </td>
                
            </tr>
            <tr>
                
                <td>
                <center>
                    <asp:HyperLink ID="HyperLink4" runat="server" Font-Size="Large" 
                        NavigateUrl="~/WebFiles/TestReport/Report.aspx" ToolTip="查询导出报表">Report</asp:HyperLink>
                </center>
                </td>
            </tr>
            <tr>
                
                <td>
                <center>
                    <asp:HyperLink ID="HyperLink2" runat="server" Font-Size="Large" 
                        NavigateUrl="~/WebFiles/OperateLog/UserOpInfo.aspx" ToolTip="查询用户使用操作记录">OPLogsInfo</asp:HyperLink>
                </center>
                </td>
            </tr>
        </table>
        </center>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="OptionContent" Runat="Server">
</asp:Content>

