﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BaseChipList.ascx.cs" Inherits="ASCXChipBaseChipList" %>
<div  >
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
<tr>
   <th ID="TH0Title" class="tHStyleLeft" width="5px" runat="server"></th>
   <th ID="TH1Title" class="tHStyleLeft" width="136px"  runat="server">
       <asp:Label ID="TH1" class="tdLeft" runat="server" Text="Label"></asp:Label>
   </th>
   <th ID="TH2Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH2" class="tdLeft" runat="server" Text="Label"></asp:Label>
   </th>
   <th ID="TH3Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH3" class="tdLeft" runat="server" Text="Label"></asp:Label>
   </th>
    <th ID="TH4Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH4" class="tdLeft" runat="server" Text="Label"></asp:Label>
   </th>
  
   <th ID="TH5Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH5" class="tdLeft" runat="server" Text="Lable"></asp:Label>
   </th>
  <th ID="TH6Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH6" class="tdLeft" runat="server" Text="Register"></asp:Label>
   </th>
</tr>
<tr id="ContentTR" runat="server">
<td width="5px" class="tdLeft" style="border:solid #000 1px">
<asp:CheckBox ID="IsSelected" runat="server" AutoPostBack="false"  />
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:LinkButton ID="TextTH1"  runat="server" >LinkButton</asp:LinkButton>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="TextTH2"  runat="server" Text="Label"></asp:Label>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<div id="AllDescription" runat="server" class="hideText" title="">
<asp:Label ID="TextTH3"  runat="server" Text="Label"></asp:Label>
</div>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="TextTH4"  runat="server" Text="Label"></asp:Label>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="TextTH5"  runat="server" Text="Label"></asp:Label>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:LinkButton ID="LBFormula"  runat="server">View</asp:LinkButton>
</td>
</tr>
</table>
</div>