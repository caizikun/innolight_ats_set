<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufConfigInitList.ascx.cs" Inherits="ASCXManufConfigInitList" %>
<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;" > 
    <tr>
    <th  width="5px" ID="TH0Title" class="tHStyleLeft" runat="server"></th>
    <th width="100px" ID="TH1Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH1" runat="server" Text="Label">Index</asp:Label>
    </th>
    <th width="100px" ID="TH2Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
    </th>
     <th width="100px" ID="TH3Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
    </th>
     <th width="100px" ID="TH4Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
    </th>
    <th width="100px" ID="TH5Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
    </th>
    <th width="100px" ID="TH6Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH6" runat="server" Text="Label"></asp:Label>
    </th>
    </tr>    
    <tr class="tdLeft" id="ContentTR" runat="server">
    <td width="5px" class="tdLeft" style="border:solid #000 1px"><asp:CheckBox  ID="IsSelected" runat="server" /></td>        
    <td width="100px" class="tdLeft" style="border:solid #000 1px"><asp:LinkButton ID="LinkBItemName"  runat="server" 
            onclick="LinkBItemName_Click">LinkButton</asp:LinkButton> </td>    
    <td   width="100px" class="tdLeft" style="border:solid #000 1px"><asp:Label ID="Colum2Text" runat="server" Text="Label" text-align="center"></asp:Label></td>
    <td  width="100px" class="tdLeft" style="border:solid #000 1px"><asp:Label ID="Colum3Text" runat="server" Text="Label" text-align="center"></asp:Label></td>
    <td  width="100px" class="tdLeft" style="border:solid #000 1px"><asp:Label ID="Colum4Text" runat="server" Text="Label" text-align="center"></asp:Label></td>
    <td width="100px" class="tdLeft" style="border:solid #000 1px"><asp:Label ID="Colum5Text" runat="server" Text="Label" text-align="center"></asp:Label></td>
    <td  width="100px" class="tdLeft" style="border:solid #000 1px"><asp:Label ID="Colum6Text" runat="server" Text="Label" text-align="center"></asp:Label></td>
    </tr>
    </table>
</div>