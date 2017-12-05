<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufConfigInitList.ascx.cs" Inherits="ASCXManufConfigInitList" %>
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
    <td  width="25px" ID="TH0Title" class="tHStyleCenter" runat="server"></td>
    <td width="200px" ID="TH1Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH1" runat="server" Text="Label">名称</asp:Label>
    </td>
    <td width="100px" ID="TH2Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
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
    <td width="100px" ID="TH6Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH6" runat="server" Text="Label"></asp:Label>
    </td>
    </tr>    
    <tr id="ContentTR" runat="server" style=" background-color:White;">
    <td width="25px" class="tdCenter"><asp:CheckBox  ID="IsSelected" runat="server" /></td>        
    <td width="200px" class="tdLeft"><asp:LinkButton ID="LinkBItemName"  runat="server" 
            onclick="LinkBItemName_Click">LinkButton</asp:LinkButton> </td>    
    <td   width="100px" class="tdCenter"><asp:Label ID="Colum2Text" runat="server" Text="Label" text-align="center"></asp:Label></td>
    <td  width="100px" class="tdCenter"><asp:Label ID="Colum3Text" runat="server" Text="Label" text-align="center"></asp:Label></td>
    <td  width="100px" class="tdCenter"><asp:Label ID="Colum4Text" runat="server" Text="Label" text-align="center"></asp:Label></td>
    <td width="100px" class="tdCenter"><asp:Label ID="Colum5Text" runat="server" Text="Label" text-align="center"></asp:Label></td>
    <td  width="100px" class="tdCenter"><asp:Label ID="Colum6Text" runat="server" Text="Label" text-align="center"></asp:Label></td>
    </tr>
    </table>
</div>