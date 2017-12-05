<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopPNSpecsList.ascx.cs" Inherits="ASCXTopPNSpecsList" %>
<div>
    <table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;" > 
    <tr>  
    <th width="5px" class="tHStyleLeft" id="THSelected" runat="server"></th>
    <th width="120px" ID="TH2Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH2" runat="server" Text="SpecName"></asp:Label>
    </th>
    <th width="100px" ID="TH6Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH6" runat="server" Text="Unit"></asp:Label>
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
   <th width="100px" ID="TH7Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>
    </th> 
    </tr>    
    <tr id="ContentTR" runat="server"> 
    <td class="tdLeft" width="5px" style="border:solid #000 1px" runat="server" id="tdSelected"><asp:CheckBox ID="IsSelected" runat="server" /></td>           
    <td width="120px" style="border:solid #000 1px"><asp:LinkButton ID="Cloum2Text" runat="server">LinkButton</asp:LinkButton> </td>  
     <td width="100px" style="border:solid #000 1px"><asp:Label ID="Cloum6Text" runat="server" Text="Label"></asp:Label></td>  
    <td width="100px" style="border:solid #000 1px"><asp:Label ID="Cloum3Text" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" style="border:solid #000 1px"><asp:Label ID="Cloum4Text" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" style="border:solid #000 1px"><asp:Label ID="Cloum5Text" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" style="border:solid #000 1px"><asp:Label ID="Cloum7Text" runat="server" Text="Label"></asp:Label></td>
    </tr>
    </table>
</div>