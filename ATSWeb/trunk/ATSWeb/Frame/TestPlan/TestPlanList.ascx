﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestPlanList.ascx.cs" Inherits="ASCXTestPlanList" %>
<div  >
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;table-layout:fixed; width:855px; ">
    <tr>
    <td width="25px"></td>        
    <td width="250px"></td>        
    <td width="100px"></td>
    <td width="100px"></td>
    <td width="100px"></td>
    <td width="200px"></td>  
    <td width="80px"></td>  
    </tr>
<tr>
   <td ID="TH0Title" class="tHStyleCenter" width="25px" runat="server"></td>
   <td ID="TH1Title" class="tHStyleCenter" width="250px"  runat="server">
       <asp:Label ID="TH1" runat="server" Text="Label"></asp:Label>
   </td>
   <td ID="TH2Title" class="tHStyleCenter" width="100px" runat="server">
       <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
   </td>
   <td ID="TH3Title" class="tHStyleCenter" width="100px" runat="server">
       <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
   </td>
    <td ID="TH8Title" class="tHStyleCenter" width="100px" runat="server">
       <asp:Label ID="TH8" runat="server" Text="Label"></asp:Label>
   </td>
   <td ID="TH4Title" class="tHStyleCenter" width="200px" runat="server">
       <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
   </td>
   <td ID="TH5Title" class="tHStyleCenter" width="80px" runat="server">
       <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
   </td>
</tr>
<tr id="ContentTR" runat="server" style=" background-color:White;" >
<td width="25px" class="tdCenter">
<asp:CheckBox ID="IsSelected" runat="server" AutoPostBack="false"  />
</td>
<td width="250px" class="tdLeft">
<asp:LinkButton ID="LBTestplanSelf"  runat="server" onclick="LBTestplanSelf_Click">LinkButton</asp:LinkButton>
</td>
<td width="100px" class="tdCenter">
<asp:Label ID="LabelSWVer"  runat="server" Text="Label"></asp:Label>
</td>
<td width="100px" class="tdCenter">
<asp:Label ID="LabelHWVer"  runat="server" Text="Label"></asp:Label>
</td>
<td width="100px" class="tdCenter">
<asp:Label ID="LabelVersion"  runat="server" Text="Label"></asp:Label>
</td>
<td width="200px" class="tdLeft">
<asp:Label ID="LBDescription" runat="server" Text=""></asp:Label>
</td>  
<td width="80px" class="tdCenter">
  <asp:Label ID="onlineLabel" runat="server" Text="—" Visible="False"></asp:Label>
  <asp:Image ID="onlineImage" runat="server" ImageUrl="~/Images/online.png" 
        Visible="False" ToolTip="" />
</td>
</tr>
</table>
</div>