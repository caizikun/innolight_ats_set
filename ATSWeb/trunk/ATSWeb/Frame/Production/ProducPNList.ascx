<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProducPNList.ascx.cs" Inherits="ASCXProducPNList" %>
<div>
    <table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse; table-layout:fixed; width:785px; "> 
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="80px"></td>
    <td width="80px"></td>
    <td width="100px"></td>
    <td  width="120px"></td>  
    <td width="200px"></td>  
    </tr>
    <tr>
    <td width="25px" ID="TH0Title" class="tHStyleCenter" runat="server"></td>
    <td  width="200px" ID="TH2Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
    </td>
    <td  width="80px" ID="TH4Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
    </td>
     <td  width="80px" ID="TH5Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
    </td>
    <td   width="100px" ID="TH6Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH6" runat="server" Text="Label"></asp:Label>
    </td>
    <td  width="120px" ID="TH7Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>
    </td>
    <td width="200px" ID="TH3Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
    </td>
    </tr>    
    <tr id="ContentTR" runat="server" style=" background-color:White;" >
    <td class="tdCenter" width="25px"><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td class="tdLeft" width="200px"><asp:LinkButton ID="TH2Text" runat="server" onclick="TH2Text_Click">LinkButton</asp:LinkButton> </td>        
    <td class="tdCenter" width="80px"><asp:Label ID="TH4Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdCenter" width="80px"><asp:Label ID="TH5Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdCenter" width="100px"><asp:Label ID="TH6Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdLeft" width="120px"><asp:Label ID="TH7Text" runat="server" Text="Label"></asp:Label></td>  
    <td class="tdLeft" width="200px"><asp:Label ID="TH3Text" runat="server" Text="Label"></asp:Label></td>  
    </tr>
    </table>
</div>