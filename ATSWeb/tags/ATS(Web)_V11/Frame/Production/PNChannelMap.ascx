<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PNChannelMap.ascx.cs" Inherits="Frame_Production_PNChannelMap" %>
<div>
    <table  cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;"> 
    <tr>
    <th width="5px" ID="TH0Title" class="tHStyleLeft" runat="server"></th>
    <th  width="130px" ID="TH2Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH2" runat="server" Text="PName"></asp:Label>
    </th>
    <th width="130px" ID="TH3Title" class="tHStyleLeft" runat="server" >
      <asp:Label ID="TH3" runat="server" Text="ModuleLine"></asp:Label>
        </th>
     <th  width="130px" ID="TH4Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH4" runat="server" Text="ChipLine"></asp:Label>
    </th>
   <th  width="130px" ID="TH5Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH5" runat="server" Text="DebugLine"></asp:Label>
    </th>
    
    </tr>    
    <tr id="ContentTR" runat="server">
    <td class="tdLeft" width="5px" style="border:solid #000 1px"><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td class="tdLeft" width="130px" style="border:solid #000 1px"><asp:LinkButton ID="TH2Text" runat="server">LinkButton</asp:LinkButton> </td>    
    <td class="tdLeft" width="130px" style="border:solid #000 1px"><asp:Label ID="TH3Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdLeft" width="130px" style="border:solid #000 1px"><asp:Label ID="TH4Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdLeft" width="130px" style="border:solid #000 1px"><asp:Label ID="TH5Text" runat="server" Text="Label"></asp:Label></td>
    </tr>
    </table>
</div>