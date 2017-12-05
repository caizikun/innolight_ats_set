<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModelRelationList.ascx.cs" Inherits="Frame_APPModel_ModelRelationList" %>
<div>
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;table-layout:fixed; width:300px;">
    <tr>
    <td width="25px"></td>                
    <td width="200px"></td>
    <td width="75px"></td>        
    </tr>
<tr>
  <td ID="TH0Title" class="tHStyleCenter" width="25px" runat="server"></td>
   <td ID="TH1Title" class="tHStyleCenter" width="200px" runat="server">
       <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
   </td>
   <td ID="TH2Title" class="tHStyleCenter" width="75px" runat="server">
       <asp:Label ID="TH2" runat="server" Text="权重"></asp:Label>
   </td>   
</tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
<td width="25px" class="tdCenter">
    <asp:CheckBox ID="chkIDModelRelation" runat="server" />
    </td>
    <td width="200px" class="tdLeft">
    <asp:Label ID="txtModelName" runat="server"  Enabled="False" >ModelName</asp:Label>    
    </td>
    <td width="75px" class="tdCenter">
    <asp:Label ID="txtModelWeight" runat="server"  Enabled="False" >ModelWeight</asp:Label>    
    </td>
</tr>
</table>
</div>
    
    
