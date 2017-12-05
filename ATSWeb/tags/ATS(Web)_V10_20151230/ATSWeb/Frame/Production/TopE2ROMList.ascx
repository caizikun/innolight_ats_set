<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopE2ROMList.ascx.cs" Inherits="ASCXTopE2ROMList" %>
<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;" >
<tr>
 <th width="5px" ID="TH0Title" class="tHStyleLeft" runat="server"></th>
 <th ID="TH2Title" class="tHStyleLeft" runat="server" width="100px">
  <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
 </th>

 
 <th ID="TH3Title" class="tHStyleLeft" runat="server" width="100px">
     <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
 </th>
 <th ID="TH4Title" class="tHStyleLeft" runat="server" width="100px">
     <asp:Label ID="TH4" runat="server" Text="CRCData0"></asp:Label>
 </th>
  <th ID="TH5Title" class="tHStyleLeft" runat="server" width="100px">
     <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
 </th>
  <th ID="TH6Title" class="tHStyleLeft" runat="server" width="100px">
     <asp:Label ID="TH6" runat="server" Text="CRCData1"></asp:Label>
 </th>
  <th ID="TH7Title" class="tHStyleLeft" runat="server" width="100px">
     <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>
 </th>
  <th ID="TH8Title" class="tHStyleLeft" runat="server" width="100px">
     <asp:Label ID="TH8" runat="server" Text="CRCData2"></asp:Label>
 </th>
 <th ID="TH9Title" class="tHStyleLeft" runat="server" width="100px">
     <asp:Label ID="TH9" runat="server" Text="Label"></asp:Label>
 </th>
 <th ID="TH10Title" class="tHStyleLeft" runat="server" width="100px">
     <asp:Label ID="TH10" runat="server" Text="CRCData3"></asp:Label>
 </th>
 </tr>
<tr id="ContentTR" runat="server">
<td style="border:solid #000 1px" width="5px">
  <asp:CheckBox ID="IsSelected"  Text="" runat="server" />
 </td>
 <td style="border:solid #000 1px" width="100px">
<asp:LinkButton ID="LinkButtonSelfInfor" runat="server" Text=""></asp:LinkButton>
</td>
 <td style="border:solid #000 1px" width="100px">
<asp:LinkButton ID="LinkButtonData0" runat="server" >View</asp:LinkButton>
</td>
<td style="border:solid #000 1px" width="100px">
 <asp:Label ID="Data0Status" runat="server" Text="Label"></asp:Label>
</td>
 <td style="border:solid #000 1px" width="100px">
<asp:LinkButton ID="LinkButtonData1" runat="server" >View</asp:LinkButton>
</td>
<td style="border:solid #000 1px" width="100px">
 <asp:Label ID="Data1Status" runat="server" Text="Label"></asp:Label>
</td>
 <td style="border:solid #000 1px" width="100px">
<asp:LinkButton ID="LinkButtonData2" runat="server" >View</asp:LinkButton>
</td>
<td style="border:solid #000 1px" width="100px">
 <asp:Label ID="Data2Status" runat="server" Text="Label"></asp:Label>
</td>
<td style="border:solid #000 1px" width="100px">
<asp:LinkButton ID="LinkButtonData3" runat="server" >View</asp:LinkButton>
</td>
<td style="border:solid #000 1px" width="100px">
 <asp:Label ID="Data3Status" runat="server" Text="Label"></asp:Label>
</td>
</tr>
</table>
</div>