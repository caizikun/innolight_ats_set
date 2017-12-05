<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ascxFileList.ascx.cs" Inherits="ASCXascxFileList" %>
<div>
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
<tr>   
<th width="5px" ID="TH0Title" class="tHStyleLeft" runat="server"></th>
   <th width="150px" ID="TH1Title" class="tHStyleLeft" runat="server">
       <asp:Label ID="TH1" class="tdLeft" runat="server" Text="ItemName"></asp:Label>
   </th>
   <th width="150px" ID="TH2Title" class="tHStyleLeft" runat="server">
       <asp:Label ID="TH2" class="tdLeft" runat="server" Text="Remarks"></asp:Label>
   </th>
 
</tr>
<tr  id="ContentTR" runat="server">
        <td width="5px" class="tdLeft" style="border:solid #000 1px">
            <asp:CheckBox ID="CheckBox1" runat="server" />
        </td>
        <td width="150px" class="tdLeft" style="border:solid #000 1px">
            <asp:LinkButton ID="lnkItemName"
                runat="server">ItemName</asp:LinkButton>
         
        </td>
        <td width="150px" class="tdLeft" style="border:solid #000 1px">
            <asp:Label ID="LableName" runat="server"></asp:Label>          
        </td>       
</tr>
</table>
</div>