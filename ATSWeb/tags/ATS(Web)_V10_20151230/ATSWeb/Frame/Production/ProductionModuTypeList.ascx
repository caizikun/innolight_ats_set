<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductionModuTypeList.ascx.cs" Inherits="ASCXProductionModuTypeList" %>
<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;" >
<tr>
 <th ID="TH0Title" class="tHStyleLeft" runat="server" width="5px"></th>
 <th ID="TH1Title" class="tHStyleLeft" runat="server" width="200px">
  <asp:Label ID="TH1" runat="server" Text="Label"></asp:Label>
 </th>
 <th ID="TH2Title" class="tHStyleLeft" runat="server" width="200px">    
  <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
 </th>
 
 <th ID="TH4Title" class="tHStyleLeft" runat="server" width="200px">
     <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
 </th>
 </tr>
<tr id="ContentTR" runat="server">
<td style="border:solid #000 1px">
  <asp:CheckBox ID="IsSelected"  Text="" runat="server" />
 </td>
 <td style="border:solid #000 1px" width="200px">
<asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
</td>
<td style="border:solid #000 1px" width="200px">
 <asp:Label ID="Lb" runat="server" Text="Label"></asp:Label>
</td>

 <td style="border:solid #000 1px" width="200px">
<asp:LinkButton ID="LinkButton2" runat="server" >LinkButton</asp:LinkButton>
</td>
</tr>
</table>
</div>