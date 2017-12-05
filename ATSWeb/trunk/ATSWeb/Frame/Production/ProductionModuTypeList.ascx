<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductionModuTypeList.ascx.cs" Inherits="ASCXProductionModuTypeList" %>
<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:325px;" >
    <tr>
    <td width="25px"></td>        
    <td width="150px"></td>        
    <td width="150px"></td>
    </tr>
<tr>
 <td ID="TH0Title" class="tHStyleCenter" runat="server" width="25px"></td>
 <td ID="TH1Title" class="tHStyleCenter" runat="server" width="150px">
  <asp:Label ID="TH1" runat="server" Text="Label"></asp:Label>
 </td>
 <td ID="TH2Title" class="tHStyleCenter" runat="server" width="150px">    
  <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
 </td>
 </tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
<td class="tdCenter" width="25px">
  <asp:CheckBox ID="IsSelected"  Text="" runat="server" />
 </td>
 <td class="tdLeft" width="150px">
<asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
</td>
<td class="tdLeft" width="150px">
 <asp:Label ID="Lb" runat="server" Text="Label"></asp:Label>
</td>
</tr>
</table>
</div>