<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestModelList.ascx.cs" Inherits="ASCXTestModelList" %>
<%@ Register src="UPDownButton.ascx" tagname="UPDownButton" tagprefix="uc1" %>
<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
<tr>
 <th width="5px" ID="TH0Title" class="tHStyleLeft" runat="server"></th>
    <th width="150px" ID="TH1Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH1" runat="server" Text="ModelName"></asp:Label>
    </th>
    <th width="150px" ID="TH2Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH2" runat="server" Text="SEQ"></asp:Label>
    </th>
     <th width="150px" ID="TH3Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH3" runat="server" Text="State"></asp:Label>
    </th>
    <th width="100px" ID="TH4Title" class="tHStyleLeft" runat="server">Up/Down</th>
</tr>
<tr id="ContentTR" runat="server">
   <td width="5px" style="border:solid #000 1px"><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td width="150px" style="border:solid #000 1px"><asp:LinkButton ID="LinkBItemName" runat="server"></asp:LinkButton> </td>    
    <td width="150px" style="border:solid #000 1px"><asp:Label ID="LbSEQ" runat="server" Text="Label"></asp:Label></td>
    <td width="150px" style="border:solid #000 1px"><asp:Label ID="LbState" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" style="border:solid #000 1px">

        <uc1:UPDownButton ID="UPDownButton1" runat="server" />

    </td>
</tr>
</table>
</div>
