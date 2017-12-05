<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportHeaderList.ascx.cs" Inherits="ASCXReportHeaderList" %>
<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:465px;" >
    <tr>
    <td width="25px"></td>        
    <td width="150px"></td>        
    <td width="200px"></td>
    <td width="90px"></td>
    </tr>
<tr>
 <td ID="TH0Title" class="tHStyleCenter" runat="server" width="25px"></td>
 <td ID="TH1Title" class="tHStyleCenter" runat="server" width="150px">
  <asp:Label ID="TH1" runat="server" Text="Label"></asp:Label>
 </td>
 <td ID="TH2Title" class="tHStyleCenter" runat="server" width="200px">    
  <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
 </td>
<td ID="TH3Title" class="tHStyleCenter" width="90px" runat="server">
    <asp:Label ID="TH3" runat="server" Text="表头格式"></asp:Label>
</td>
 </tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
<td class="tdCenter" width="25px">
  <asp:CheckBox ID="IsSelected"  Text="" runat="server" />
 </td>
 <td class="tdLeft" width="150px">
<asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
</td>
<td class="tdLeft" width="200px">
 <asp:Label ID="Lb" runat="server" Text="Label"></asp:Label>
</td>
<td width="90px" class="tdCenter">
    <asp:LinkButton ID="lnkViewParams"
        runat="server"> 查看 </asp:LinkButton>
</td>
</tr>
</table>
</div>