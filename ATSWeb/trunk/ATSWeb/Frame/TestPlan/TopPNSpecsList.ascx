<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopPNSpecsList.ascx.cs" Inherits="ASCXTopPNSpecsList" %>
<div>
    <table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:725px;" > 
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="100px"></td>
    <td width="100px"></td>
    <td width="100px"></td>
    <td width="100px"></td>
    <td width="100px"></td>
    </tr>

    <tr>  
    <td width="25px" class="tHStyleLeft" id="THSelected" runat="server"></td>
    <td width="200px" ID="TH2Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH2" runat="server" Text="名称"></asp:Label>
    </td>
    <td width="100px" ID="TH6Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH6" runat="server" Text="单位"></asp:Label>
    </td>
    <td width="100px" ID="TH3Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
    </td>
     <td width="100px" ID="TH4Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
    </td>
     <td width="100px" ID="TH5Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
    </td>   
   <td width="100px" ID="TH7Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>
    </td> 
    </tr>    
    <tr id="ContentTR" runat="server" style=" background-color:White;"> 
    <td width="25px" class="tdCenter" runat="server" id="tdSelected"><asp:CheckBox ID="IsSelected" runat="server" /></td>           
    <td width="200px" class="tdLeft"><asp:LinkButton ID="Cloum2Text" runat="server">LinkButton</asp:LinkButton> </td>  
     <td width="100px" class="tdCenter"><asp:Label ID="Cloum6Text" runat="server" Text="Label"></asp:Label></td>  
    <td width="100px" class="tdCenter"><asp:Label ID="Cloum3Text" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" class="tdCenter"><asp:Label ID="Cloum4Text" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" class="tdCenter"><asp:Label ID="Cloum5Text" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" class="tdCenter"><asp:Label ID="Cloum7Text" runat="server" Text="Label"></asp:Label></td>
    </tr>
    </table>
</div>