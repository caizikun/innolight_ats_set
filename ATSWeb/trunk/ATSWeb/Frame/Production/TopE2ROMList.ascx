<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopE2ROMList.ascx.cs" Inherits="ASCXTopE2ROMList" %>
<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:945px; " >
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="80px"></td>
    <td width="100px"></td>
    <td width="80px"></td>
    <td width="100px"></td>  
    <td width="80px"></td>
    <td width="100px"></td>
    <td width="80px"></td>
    <td width="100px"></td>   
    </tr>
<tr>
 <td width="25px" ID="TH0Title" class="tHStyleCenter" runat="server"></td>
 <td ID="TH2Title" class="tHStyleCenter" runat="server" width="200px">
  <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
 </td>
 <td ID="TH3Title" class="tHStyleCenter" runat="server" width="80px">
     <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
 </td>
 <td ID="TH4Title" class="tHStyleCenter" runat="server" width="100px">
     <asp:Label ID="TH4" runat="server" Text="Data0校验"></asp:Label>
 </td>
  <td ID="TH5Title" class="tHStyleCenter" runat="server" width="80px">
     <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
 </td>
  <td ID="TH6Title" class="tHStyleCenter" runat="server" width="100px">
     <asp:Label ID="TH6" runat="server" Text="Data1校验"></asp:Label>
 </td>
  <td ID="TH7Title" class="tHStyleCenter" runat="server" width="80px">
     <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>
 </td>
  <td ID="TH8Title" class="tHStyleCenter" runat="server" width="100px">
     <asp:Label ID="TH8" runat="server" Text="Data2校验"></asp:Label>
 </td>
 <td ID="TH9Title" class="tHStyleCenter" runat="server" width="80px">
     <asp:Label ID="TH9" runat="server" Text="Label"></asp:Label>
 </td>
 <td ID="TH10Title" class="tHStyleCenter" runat="server" width="100px">
     <asp:Label ID="TH10" runat="server" Text="Data3校验"></asp:Label>
 </td>
 </tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
<td class="tdCenter" width="25px">
  <asp:CheckBox ID="IsSelected"  Text="" runat="server" />
 </td>
 <td class="tdLeft" width="200px">
<asp:LinkButton ID="LinkButtonSelfInfor" runat="server" Text=""></asp:LinkButton>
</td>
 <td class="tdCenter" width="80px">
<asp:LinkButton ID="LinkButtonData0" runat="server" >查看</asp:LinkButton>
</td>
<td class="tdCenter" width="100px">
 <asp:Label ID="Data0Status" runat="server" Text="Label"></asp:Label>
</td>
 <td class="tdCenter" width="80px">
<asp:LinkButton ID="LinkButtonData1" runat="server" >查看</asp:LinkButton>
</td>
<td class="tdCenter" width="100px">
 <asp:Label ID="Data1Status" runat="server" Text="Label"></asp:Label>
</td>
 <td class="tdCenter" width="80px">
<asp:LinkButton ID="LinkButtonData2" runat="server" >查看</asp:LinkButton>
</td>
<td class="tdCenter" width="100px">
 <asp:Label ID="Data2Status" runat="server" Text="Label"></asp:Label>
</td>
<td class="tdCenter" width="80px">
<asp:LinkButton ID="LinkButtonData3" runat="server" >查看</asp:LinkButton>
</td>
<td class="tdCenter" width="100px">
 <asp:Label ID="Data3Status" runat="server" Text="Label"></asp:Label>
</td>
</tr>
</table>
</div>