<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PNType.ascx.cs" Inherits="ASCXPNType" %>
<div >
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
   <tr>
 <th width="200px"  height="29px" ID="TH2Title" class="tHStyleLeft" runat="server">
  <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
 </th>
 <th width="100px" ID="TH3Title" class="tHStyleLeft" runat="server" >    
  <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
 </th>
 <th width="100px" ID="TH4Title" class="tHStyleLeft" runat="server">    
  <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
 </th>
 <th width="100px" ID="TH5Title" class="tHStyleLeft" runat="server">    
  <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
 </th>
 <th width="100px" ID="TH6Title" class="tHStyleLeft" runat="server" >    
  <asp:Label ID="TH6" runat="server" Text="Label"></asp:Label>
 </th>
 <th width="300px" ID="TH7Title" class="tHStyleLeft" runat="server" >    
  <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>
 </th>
</tr>
<tr id="ContentTR" runat="server">
<td style="border:solid #000 1px" width="120px">
<asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">LinkButton</asp:LinkButton>
</td>
<td style="border:solid #000 1px" width="100px">
    <asp:Label ID="TH3Text" runat="server" Text="Label"></asp:Label>
</td>
<td style="border:solid #000 1px" width="100px">
    <asp:Label ID="TH4Text" runat="server" Text="Label"></asp:Label>
</td>
<td style="border:solid #000 1px" width="100px">
    <asp:Label ID="TH5Text" runat="server" Text="Label"></asp:Label>
</td>
<td style="border:solid #000 1px" width="100px">
    <asp:Label ID="TH6Text" runat="server" Text="Label"></asp:Label>
</td>
<td style="border:solid #000 1px" width="300px">
    <asp:Label ID="TH7Text" runat="server" Text="Label"></asp:Label>
</td>
</tr>
</table>

</div>