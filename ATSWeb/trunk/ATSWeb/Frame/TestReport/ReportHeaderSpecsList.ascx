<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportHeaderSpecsList.ascx.cs" Inherits="ASCXReportHeaderSpecsList" %>
<%@ Register src="~/Frame/TestPlan/UPDownButton.ascx" tagname="UPDownButton" tagprefix="uc1" %>
<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:525px;">
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="200px"></td>
    <td width="100px"></td>
    </tr>
<tr>
 <td width="25px" ID="TH0Title" class="tHStyleCenter" runat="server"></td>
    <td width="200px" ID="TH1Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
    </td>
    <td width="200px" ID="TH2Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH2" runat="server" Text="显示别名"></asp:Label>
    </td>
    <td width="100px" ID="TH4Title" class="tHStyleCenter" runat="server">序号</td>
</tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
   <td width="25px" class="tdCenter"><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td width="200px" class="tdLeft"><asp:LinkButton ID="LinkBItemName" runat="server"></asp:LinkButton> </td>  
    <td width="200px" class="tdLeft"><asp:Label ID="LbShowName" runat="server" Text=""></asp:Label></td>  
    <td width="100px" class="tdCenter"><asp:Label ID="LbSEQ" runat="server" Text="Label"></asp:Label></td>
</tr>
</table>
</div>
