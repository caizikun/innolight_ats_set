<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModelRelationAddList.ascx.cs" Inherits="ASCXModelRelationAddList" %>

<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:225px;">
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    </tr>
<tr>
 <td width="25px" ID="TH0Title" class="tHStyleCenter" runat="server"></td>
    <td width="200px" ID="TH1Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
    </td>
</tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
   <td width="25px" class="tdCenter"><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td width="200px" class="tdLeft"><asp:Label ID="LinkBItemName" runat="server"></asp:Label> </td>  
</tr>
</table>
</div>
