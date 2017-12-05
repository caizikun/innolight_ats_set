<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PNChannelMap.ascx.cs" Inherits="Frame_Production_PNChannelMap" %>
<div>
    <table  cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:525px; "> 
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="100px"></td>
    <td width="100px"></td>
    <td width="100px"></td> 
    </tr>
    <tr>
    <td width="25px" ID="TH0Title" class="tHStyleCenter" runat="server"></td>
    <td  width="200px" ID="TH2Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH2" runat="server" Text="品名"></asp:Label>
    </td>
    <td width="100px" ID="TH3Title" class="tHStyleCenter" runat="server" >
      <asp:Label ID="TH3" runat="server" Text="模块通道"></asp:Label>
        </td>
     <td  width="100px" ID="TH4Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH4" runat="server" Text="芯片通道"></asp:Label>
    </td>
   <td  width="100px" ID="TH5Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH5" runat="server" Text="调试通道"></asp:Label>
    </td>
    
    </tr>    
    <tr id="ContentTR" runat="server" style=" background-color:White;">
    <td class="tdCenter" width="25px"><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td class="tdLeft" width="200px"><asp:LinkButton ID="TH2Text" runat="server">LinkButton</asp:LinkButton> </td>    
    <td class="tdCenter" width="100px"><asp:Label ID="TH3Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdCenter" width="100px"><asp:Label ID="TH4Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdCenter" width="100px"><asp:Label ID="TH5Text" runat="server" Text="Label"></asp:Label></td>
    </tr>
    </table>
</div>