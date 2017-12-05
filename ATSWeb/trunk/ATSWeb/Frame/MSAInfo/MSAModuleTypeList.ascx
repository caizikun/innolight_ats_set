<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MSAModuleTypeList.ascx.cs" Inherits="ASCXMSAModuleTypeList" %>
<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:595px;" >
    <tr>
    <td width="25px"></td>        
    <td width="180px"></td>        
    <td width="120px"></td>
    <td width="120px"></td>        
    <td width="150px"></td>
    </tr>
<tr>
 <td width="25px"  ID="TH0Title" class="tHStyleCenter" runat="server"></td>
 <td  ID="TH1Title" class="tHStyleCenter" runat="server" width="180px">
  <asp:Label ID="TH1" runat="server" Text="Label"></asp:Label>
 </td>
 <td  ID="TH2Title" class="tHStyleCenter" runat="server" width="120px">    
  <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
 </td>
 <td  ID="TH3Title" class="tHStyleCenter" runat="server" width="120px">
     <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
 </td>
 
 <td  ID="TH5Title" class="tHStyleCenter" runat="server" width="150px">
     <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
 </td>
 </tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
<td width="25px" class="tdCenter">
  <asp:CheckBox ID="IsSelected"  Text="" runat="server" />
 </td>
 <td class="tdLeft" width="180px">
<asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
</td>
<td class="tdLeft" width="120px">
 <asp:Label ID="Lb" runat="server" Text="Label"></asp:Label>
</td>
<td class="tdLeft" width="120px">
 <asp:Label ID="Lb2" runat="server" Text="Label"></asp:Label>
</td>

 <td class="tdCenter" width="150px">
<asp:LinkButton ID="LinkButton2" runat="server" >LinkButton</asp:LinkButton>
</td>

</tr>
</table>
</div>