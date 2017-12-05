<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChipSetContrl.ascx.cs" Inherits="ASCXChipSetContrl" %>
<div>
    <table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;" > 
    <tr>
    <th width="5px" ID="TH0Title" class="tHStyleLeft" runat="server"></th>
    <th width="180px" ID="TH2Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
    </th>
    <th  width="130px" ID="TH3Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
    </th>
     <th width="130px" ID="TH4Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
    </th>
     <th width="150px" ID="TH5Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
    </th>
    <th width="130px" ID="TH6Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH6" runat="server" Text="Label"></asp:Label>
    </th>
    <th width="130px" ID="TH7Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>
    </th>
    
    
    </tr>    
    <tr id="ContentTR" runat="server">
    <td width="5px" style="border:solid #000 1px"><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td width="180px" style="border:solid #000 1px"><asp:LinkButton ID="TH2Text" runat="server">LinkButton</asp:LinkButton> </td>    
    <td width="130px" style="border:solid #000 1px"><asp:Label ID="TH3Text" runat="server" Text="Label"></asp:Label></td>
    <td width="130px" style="border:solid #000 1px"><asp:Label ID="TH4Text" runat="server" Text="Label"></asp:Label></td>
    <td width="150px" style="border:solid #000 1px"><asp:Label ID="TH5Text" runat="server" Text="Label"></asp:Label></td>
    <td width="130px" style="border:solid #000 1px"><asp:Label ID="TH6Text" runat="server" Text="Label"></asp:Label></td>
    <td width="130px" style="border:solid #000 1px"><asp:Label ID="TH7Text" runat="server" Text="Label"></asp:Label></td>   
      
    </tr>
    </table>
</div>