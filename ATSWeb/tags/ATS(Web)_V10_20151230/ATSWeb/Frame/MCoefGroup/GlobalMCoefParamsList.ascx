<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalMCoefParamsList.ascx.cs" Inherits="Frame_MCoefGroup_GlobalMCoefParamsList" %>

<div>
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
 <tr>
        <th ID="TH0Title" class="tHStyleLeft" width="5px"  runat="server">
        </th>
        <th ID="TH1Title" class="tHStyleLeft" width="200px" runat="server">
         <asp:Label ID="TH1" runat="server" Text="Type"></asp:Label>
        </th>
        <th ID="TH2Title" class="tHStyleLeft" width="200px" runat="server">
        <asp:Label ID="TH2" runat="server" Text="Item"></asp:Label>
        </th>
        <th ID="TH3Title" class="tHStyleLeft" width="100px" runat="server">
        <asp:Label ID="TH3" runat="server" Text="Channel"></asp:Label>
        </th>
        <th ID="TH4Title" class="tHStyleLeft" width="100px" runat="server">
        <asp:Label ID="TH4" runat="server" Text="Page"></asp:Label>
        </th>
         <th ID="TH5Title" class="tHStyleLeft" width="100px" runat="server">
        <asp:Label ID="TH5" runat="server" Text="StartAddress"></asp:Label>
        </th>
        <th ID="TH6Title" class="tHStyleLeft" width="100px" runat="server">
        <asp:Label ID="TH6" runat="server" Text="Length"></asp:Label>
        </th>
        <th ID="TH7Title" class="tHStyleLeft" width="100px" runat="server">
        <asp:Label ID="TH7" runat="server" Text="Format"></asp:Label>
        </th>
        </tr>
<tr id="ContentTR" runat="server">
<td width="5px" class="tdLeft" style="border:solid #000 1px">
    <asp:CheckBox ID="chkIDMCoefParam" runat="server" />
    </td>
    <td width="200px" class="tdLeft" style="border:solid #000 1px">
    <asp:Label ID="txtItemType" runat="server"  Enabled="False" >ItemType</asp:Label>    
    </td>
    <td width="200px" class="tdLeft" style="border:solid #000 1px">
    <asp:LinkButton ID="lnkItemName"
        runat="server">ItemName</asp:LinkButton>     
        </td>
    <td width="100px" class="tdLeft" style="border:solid #000 1px">
    <asp:Label ID="txtChannel" runat="server" Enabled="False" >Channel</asp:Label>
    </td>
    <td width="100px" class="tdLeft" style="border:solid #000 1px">         
    <asp:Label ID="txtPage" runat="server"  >Page</asp:Label>       
    </td>
    <td width="100px" class="tdLeft" style="border:solid #000 1px">    
    <asp:Label ID="txtStartAddress" runat="server"></asp:Label> 
    </td>
    <td width="100px" class="tdLeft" style="border:solid #000 1px">    
    <asp:Label ID="txtLength" runat="server"></asp:Label>
    </td>    
    <td width="100px" class="tdLeft" style="border:solid #000 1px">
        <asp:Label ID="txtFormat" runat="server"  ></asp:Label>
    </td>
</tr>
</table>
</div>
    
    
