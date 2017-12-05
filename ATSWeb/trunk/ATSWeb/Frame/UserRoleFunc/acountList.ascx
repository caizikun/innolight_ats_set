<%@ Control Language="C#" AutoEventWireup="true" CodeFile="acountList.ascx.cs" Inherits="ASCXacountList" %>
<div>
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;table-layout:fixed; width:405px;">
<tr>
    <td width="25px"></td>        
    <td width="180px"></td>        
    <td width="100px"></td>
    <td width="100px"></td>     
</tr>
<tr>   
<td width="25px" ID="TH0Title" class="tHStyleCenter" runat="server"></td>
   <td width="180px" ID="TH1Title" class="tHStyleCenter" runat="server">
       <asp:Label ID="TH1" runat="server" Text="用户名"></asp:Label>
   </td>
   <td width="100px" ID="TH2Title" class="tHStyleCenter" runat="server">
       <asp:Label ID="TH2" runat="server" Text="用户角色"></asp:Label>
   </td>
    <td width="100px" ID="TH3Title" class="tHStyleCenter" runat="server">
       <asp:Label ID="TH3" runat="server" Text="用户权限"></asp:Label>
   </td>

</tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
        <td width="25px" class="tdCenter">
            <asp:CheckBox ID="CheckBox1" runat="server" />
        </td>
        <td width="180px" class="tdLeft">
            <asp:LinkButton ID="lnkItemName"
                runat="server">ItemName</asp:LinkButton>
         
        </td>
        <td width="100px" class="tdCenter">
           <asp:LinkButton ID="lnk2ItemName"
                runat="server">ItemName</asp:LinkButton>  
        </td> 

        <td width="100px" id="TD3" class="tdCenter">
           <asp:LinkButton ID="lnk3ItemName"
                runat="server">查看</asp:LinkButton>  
        </td>       
</tr>
</table>
</div>