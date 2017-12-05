<%@ Control Language="C#" AutoEventWireup="true" CodeFile="acountList.ascx.cs" Inherits="ASCXacountList" %>
<div>
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
<tr>   
<th width="5px" ID="TH0Title" class="tHStyleLeft" runat="server"></th>
   <th width="150px" ID="TH1Title" class="tHStyleLeft" runat="server">
       <asp:Label ID="TH1" class="tdLeft" runat="server" Text="UserName"></asp:Label>
   </th>
   <th width="150px" ID="TH2Title" class="tHStyleLeft" runat="server">
       <asp:Label ID="TH2" class="tdLeft" runat="server" Text="UserRoles"></asp:Label>
   </th>
    <th width="150px" ID="TH3Title" class="tHStyleLeft" runat="server">
       <asp:Label ID="TH3" class="tdLeft" runat="server" Text="UserAction"></asp:Label>
   </th>

</tr>
<tr id="ContentTR" runat="server">
        <td width="5px" class="tdLeft" style="border:solid #000 1px">
            <asp:CheckBox ID="CheckBox1" runat="server" />
        </td>
        <td width="150px" class="tdLeft" style="border:solid #000 1px">
            <asp:LinkButton ID="lnkItemName"
                runat="server">ItemName</asp:LinkButton>
         
        </td>
        <td width="150px" class="tdLeft" style="border:solid #000 1px">
           <asp:LinkButton ID="lnk2ItemName"
                runat="server">ItemName</asp:LinkButton>  
        </td> 

        <td width="150px" ID="TD3" class="tdLeft" style="border:solid #000 1px">
           <asp:LinkButton ID="lnk3ItemName"
                runat="server">View</asp:LinkButton>  
        </td>       
</tr>
</table>
</div>