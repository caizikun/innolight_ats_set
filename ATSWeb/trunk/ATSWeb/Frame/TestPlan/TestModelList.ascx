<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestModelList.ascx.cs" Inherits="ASCXTestModelList" %>

<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:425px;">
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="100px"></td>
    <td width="100px"></td>
    </tr>
<tr>
 <td width="25px" ID="TH0Title" class="tHStyleCenter" runat="server"></td>
    <td width="200px" ID="TH1Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
    </td>
     <td width="100px" ID="TH3Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH3" runat="server" Text="跳过否?"></asp:Label>
    </td>
    <td width="100px" ID="TH2Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH2" runat="server" Text="排序"></asp:Label>
    </td>   
</tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
   <td width="25px" class="tdCenter"><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td width="200px" class="tdLeft"><asp:LinkButton ID="LinkBItemName" runat="server"></asp:LinkButton> </td>    
    <td width="100px" class="tdCenter"><asp:Label ID="LbState" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" class="tdCenter"><asp:Label ID="LbSEQ" runat="server" Text="Label"></asp:Label></td>
</tr>
</table>
</div>
