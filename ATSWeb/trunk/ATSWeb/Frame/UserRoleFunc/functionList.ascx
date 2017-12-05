<%@ Control Language="C#" AutoEventWireup="true" CodeFile="functionList.ascx.cs" Inherits="ASCXfunctionList" %>
<div>
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;table-layout:fixed; width:555px;">
<tr>
    <td width="25px"></td>        
    <td width="180px"></td>        
    <td width="150px"></td>
    <td width="200px"></td>     
    </tr>
<tr>   
<td width="25px" ID="TH0Title" class="tHStyleCenter" runat="server"></td>
   <td width="180px" ID="TH1Title" class="tHStyleCenter" runat="server">
       <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
   </td>
   <td width="150px" ID="TH2Title" class="tHStyleCenter" runat="server">
       <asp:Label ID="TH2" runat="server" Text="描述"></asp:Label>
   </td>
 <td width="200px" ID="TH3Title" class="tHStyleCenter" runat="server">
       <asp:Label ID="TH3" runat="server" Text="功能"></asp:Label>
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
        <td width="150px" class="tdLeft">
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </td> 
         <td width="200px" class="tdLeft">
           <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        </td>     
</tr>
</table>
</div>