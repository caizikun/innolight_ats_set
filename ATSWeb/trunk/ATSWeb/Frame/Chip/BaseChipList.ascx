<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BaseChipList.ascx.cs" Inherits="ASCXChipBaseChipList" %>
<div  >
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;table-layout:fixed; width:875px;">
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="80px"></td>
    <td width="200px"></td>        
    <td width="120px"></td>
    <td width="150px"></td>
    <td width="100px"></td>
    </tr>
<tr>
   <td ID="TH0Title" class="tHStyleCenter" width="25px" runat="server"></td>
   <td ID="TH1Title" class="tHStyleCenter" width="200px"  runat="server">
       <asp:Label ID="TH1" runat="server" Text="Label"></asp:Label>
   </td>
   <td ID="TH2Title" class="tHStyleCenter" width="80px" runat="server">
       <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
   </td>
   <td ID="TH3Title" class="tHStyleCenter" width="200px" runat="server">
       <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
   </td>
    <td ID="TH4Title" class="tHStyleCenter" width="120px" runat="server">
       <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
   </td>
  
   <td ID="TH5Title" class="tHStyleCenter" width="150px" runat="server">
       <asp:Label ID="TH5" runat="server" Text="Lable"></asp:Label>
   </td>
  <td ID="TH6Title" class="tHStyleCenter" width="100px" runat="server">
       <asp:Label ID="TH6" runat="server" Text="寄存器信息"></asp:Label>
   </td>
</tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
<td width="25px" class="tdCenter">
<asp:CheckBox ID="IsSelected" runat="server" AutoPostBack="false"  />
</td>
<td width="200px" class="tdLeft">
<asp:LinkButton ID="TextTH1"  runat="server" >LinkButton</asp:LinkButton>
</td>
<td width="80px" class="tdCenter">
<asp:Label ID="TextTH2"  runat="server" Text="Label"></asp:Label>
</td>
<td width="200px" class="tdLeft">
<div id="AllDescription" runat="server" class="hideText" title="">
<asp:Label ID="TextTH3"  runat="server" Text="Label"></asp:Label>
</div>
</td>
<td width="120px" class="tdCenter">
<asp:Label ID="TextTH4"  runat="server" Text="Label"></asp:Label>
</td>
<td width="150px" class="tdCenter">
<asp:Label ID="TextTH5"  runat="server" Text="Label"></asp:Label>
</td>
<td width="100px" class="tdCenter">
<asp:LinkButton ID="LBFormula"  runat="server">查看</asp:LinkButton>
</td>
</tr>
</table>
</div>