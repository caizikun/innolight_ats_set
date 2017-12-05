<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChipSetContrl.ascx.cs" Inherits="ASCXChipSetContrl" %>
<div>
    <table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:965px;" > 
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="100px"></td>
    <td width="100px"></td>
    <td width="120px"></td>
    <td  width="120px"></td>  
    <td width="100px"></td>  
    <td width="100px"></td> 
    <td width="100px"></td> 
    </tr>
    <tr>
    <td width="25px" ID="TH0Title" class="tHStyleCenter" runat="server"></td>
    <td width="200px" ID="TH2Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
    </td>
    <td  width="100px" ID="TH3Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
    </td>
     <td width="100px" ID="TH4Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
    </td>
     <td width="120px" ID="TH5Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
    </td>
    <td width="120px" ID="TH6Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH6" runat="server" Text="Label"></asp:Label>
    </td>
    <td width="100px" ID="TH7Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>
    </td>
    <td width="100px" ID="TH8Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH8" runat="server" Text="Label"></asp:Label>
    </td>
    <td width="100px" ID="TH9Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH9" runat="server" Text="Label"></asp:Label>
    </td>
    
    </tr>    
    <tr id="ContentTR" runat="server" style=" background-color:White;">
    <td width="25px" class="tdCenter"><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td width="200px" class="tdLeft"><asp:LinkButton ID="TH2Text" runat="server">LinkButton</asp:LinkButton> </td>    
    <td width="100px" class="tdCenter"><asp:Label ID="TH3Text" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" class="tdCenter"><asp:Label ID="TH4Text" runat="server" Text="Label"></asp:Label></td>
    <td width="120px" class="tdCenter"><asp:Label ID="TH5Text" runat="server" Text="Label"></asp:Label></td>
    <td width="120px" class="tdCenter"><asp:Label ID="TH6Text" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" class="tdCenter"><asp:Label ID="TH7Text" runat="server" Text="Label"></asp:Label></td>   
    <td width="100px" id="TH8td" class="tdCenter"><asp:Label ID="TH8Text" runat="server" Text="Label"></asp:Label></td>  
    <td width="100px" id="TH9td" class="tdCenter"><asp:Label ID="TH9Text" runat="server" Text="Label"></asp:Label></td>       
    </tr>
    </table>
</div>