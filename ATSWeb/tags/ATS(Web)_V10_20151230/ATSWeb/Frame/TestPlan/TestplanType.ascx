<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestplanType.ascx.cs" Inherits="ASCXTestplanType" %>
<div >
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
<tr >
    <th  ID="TH1Title" class="tHStyleLeft" runat="server"   width="150px" > 
  <asp:Label ID="TH1"   runat="server" Text="Label"></asp:Label> 
 </th>
 <th ID="TH2Title" class="tHStyleLeft" runat="server"   width="150px">    
  <asp:Label ID="TH2"  runat="server" Text="Label"></asp:Label>
 </th>
 </tr>
<tr id="ContentTR" runat="server">
<td   width="150px" style="border:solid #000 1px">
<asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">LinkButton</asp:LinkButton>
</td>
<td  width="150px" style="border:solid #000 1px">
    <asp:Label ID="Lb" runat="server" Text="Label"></asp:Label>
</td>

</tr>
</table>

</div>