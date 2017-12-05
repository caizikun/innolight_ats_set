<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestplanType.ascx.cs" Inherits="ASCXTestplanType" %>
<div >
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:300px;">
    <tr>       
    <td width="150px"></td>        
    <td width="150px"></td>
    </tr>
<tr >
    <td  ID="TH1Title" class="tHStyleCenter" runat="server"   width="150px" > 
  <asp:Label ID="TH1"   runat="server" Text="Label"></asp:Label> 
 </td>
 <td ID="TH2Title" class="tHStyleCenter" runat="server"   width="150px">    
  <asp:Label ID="TH2"  runat="server" Text="Label"></asp:Label>
 </td>
 </tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
<td   width="150px" class="tdLeft">
<asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">LinkButton</asp:LinkButton>
</td>
<td  width="150px" class="tdLeft">
    <asp:Label ID="Lb" runat="server" Text="Label"></asp:Label>
</td>

</tr>
</table>

</div>