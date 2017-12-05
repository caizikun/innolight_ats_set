<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProducPNList.ascx.cs" Inherits="ASCXProducPNList" %>
<div>
    <table  cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;"> 
    <tr>
    <th width="5px" ID="TH0Title" class="tHStyleLeft" runat="server"></th>
    <th  width="150px" ID="TH2Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
    </th>
    <th width="130px" ID="TH3Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
    </th>
     <th  width="100px" ID="TH4Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
    </th>
     <th  width="100px" ID="TH5Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
    </th>
    <th   width="100px" ID="TH6Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH6" runat="server" Text="Label"></asp:Label>
    </th>
    <th  width="100px" ID="TH7Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>
    </th>
     <th  width="130px" ID="TH8Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH8" runat="server" Text="ChipsetControl"></asp:Label>
    </th>
     <th  width="130px" ID="TH9Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH9" runat="server" Text="ChipsetInit"></asp:Label>
    </th>
     <th  width="130px" ID="TH10Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH10" runat="server" Text="EEPROM"></asp:Label>
    </th>
     <th  width="130px" ID="TH11Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH11" runat="server" Text="Chip"></asp:Label>
    </th>
    </tr>    
    <tr id="ContentTR" runat="server">
    <td class="tdLeft" width="5px" style="border:solid #000 1px"><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td class="tdLeft" width="150px" style="border:solid #000 1px"><asp:LinkButton ID="TH2Text" runat="server">LinkButton</asp:LinkButton> </td>    
    <td class="tdLeft" width="130px" style="border:solid #000 1px"><asp:Label ID="TH3Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdLeft" width="100px" style="border:solid #000 1px"><asp:Label ID="TH4Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdLeft" width="100px" style="border:solid #000 1px"><asp:Label ID="TH5Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdLeft" width="100px" style="border:solid #000 1px"><asp:Label ID="TH6Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdLeft" width="100px" style="border:solid #000 1px" ><asp:Label ID="TH7Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdLeft" width="130px" style="border:solid #000 1px"><asp:LinkButton ID="LinkBChipSetControl" runat="server">View</asp:LinkButton>
     </td>
   <td class="tdLeft" width="130px" style="border:solid #000 1px"><asp:LinkButton ID="LinkBChipSetIni" runat="server" >View</asp:LinkButton>
     </td> 
     <td class="tdLeft" width="130px" style="border:solid #000 1px"><asp:LinkButton ID="LinkButtonE2PROM" runat="server" >View</asp:LinkButton>
     </td> 
      <td class="tdLeft" width="130px" style="border:solid #000 1px"><asp:LinkButton ID="LinkButtonChip" runat="server" >View</asp:LinkButton>
     </td>      
    </tr>
    </table>
</div>