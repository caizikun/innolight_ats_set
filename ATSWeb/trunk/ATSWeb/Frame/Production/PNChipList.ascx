<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PNChipList.ascx.cs" Inherits="Frame_Production_PNChipList" %>
<div>
    <table  cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:525px;"> 
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
        <asp:Label ID="TH2" runat="server" Text="名称"></asp:Label>
    </td>
    <td width="100px" ID="TH3Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH3" runat="server" Text="类型"></asp:Label>
    </td>
     <td  width="100px" ID="TH4Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH4" runat="server" Text="方向"></asp:Label>
    </td>
     <td  width="100px" ID="TH5Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH5" runat="server" Text="通道映射"></asp:Label>
    </td>
    
    </tr>    
    <tr id="ContentTR" runat="server" style="background-color:White;">
    <td class="tdCenter" width="25px" ><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td class="tdLeft" width="200px" ><asp:LinkButton ID="TH2Text" runat="server">LinkButton</asp:LinkButton> </td>    
    <td class="tdLeft" width="100px" ><asp:Label ID="TH3Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdLeft" width="100px" ><asp:Label ID="TH4Text" runat="server" Text="Label"></asp:Label></td>
    <td class="tdCenter" width="100px"><asp:LinkButton ID="LinkBChannelMapControl" runat="server">查看</asp:LinkButton>
     </td>
    
    </tr>
    </table>
</div>