<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FormulaList.ascx.cs" Inherits="ASCX_Chip_FormulaList" %>
<div  >
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;table-layout:fixed; width:1040px;">
    <tr>
    <td width="25px"></td>        
    <td width="130px"></td>   
    <td width="210px"></td>       
    <td width="90px"></td>
    <td width="210px"></td>        
    <td width="80px"></td>
    <td width="65px"></td>
    <td width="65px"></td>
    <td width="85px"></td>
    <td width="80px"></td>
    </tr>
<tr>
   <td ID="TH0Title" class="tHStyleCenter" width="25px" runat="server"></td>
   <td ID="TH1Title" class="tHStyleCenter" width="130px"  runat="server">
       <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
   </td>
   <td ID="TH2Title" class="tHStyleCenter" width="210px" runat="server">
       <asp:Label ID="TH2" runat="server" Text="写公式"></asp:Label>
   </td>
   <td ID="TH3Title" class="tHStyleCenter" width="90px" runat="server">
       <asp:Label ID="TH3" runat="server" Text="模拟量单位"></asp:Label>
   </td>
    <td ID="TH4Title" class="tHStyleCenter" width="210px" runat="server">
       <asp:Label ID="TH4" runat="server" Text="读公式"></asp:Label>
   </td>
     <td ID="TH5Title" class="tHStyleCenter" width="80px" runat="server">
       <asp:Label ID="TH5" runat="server" Text="地址(Dec)"></asp:Label>
   </td>
    <td ID="TH6Title" class="tHStyleCenter" width="65px" runat="server">
       <asp:Label ID="TH6" runat="server" Text="开始位"></asp:Label>
   </td>
  <td ID="TH7Title" class="tHStyleCenter" width="65px" runat="server">
       <asp:Label ID="TH7" runat="server" Text="结束位"></asp:Label>
   </td>
   <td ID="TH8Title" class="tHStyleCenter" width="85px" runat="server">
       <asp:Label ID="TH8" runat="server" Text="单位长度"></asp:Label>
   </td>
  <td ID="TH9Title" class="tHStyleCenter" width="80px" runat="server">
       <asp:Label ID="TH9" runat="server" Text="芯片通道"></asp:Label>
   </td>
  
</tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
<td width="25px" class="tdCenter">
<asp:CheckBox ID="IsSelected" runat="server" AutoPostBack="false"  />
</td>
<td width="130px" class="tdLeft">
<asp:LinkButton ID="TextTH1"  runat="server" >LinkButton</asp:LinkButton>
</td>
<td width="210px" class="tdLeft">
<asp:Label ID="TextTH2"  runat="server" Text="Label"></asp:Label>
</td>
<td width="90px" class="tdCenter">
<asp:Label ID="TextTH3"  runat="server" Text="Label"></asp:Label>
</td>
<td width="210px" class="tdLeft">
<asp:Label ID="TextTH4"  runat="server" Text="Label"></asp:Label>
</td>
<td width="80px" class="tdCenter">
<asp:Label ID="TextTH5"  runat="server" Text="Label"></asp:Label>

</td>
<td width="65px" class="tdCenter">
<asp:Label ID="TextTH6"  runat="server" Text="Label"></asp:Label>
</td>
<td width="65px" class="tdCenter">
<asp:Label ID="TextTH7"  runat="server" Text="Label"></asp:Label>
</td>
<td width="85px" class="tdCenter">
<asp:Label ID="TextTH8"  runat="server" Text="Label"></asp:Label>
</td>
<td width="80px" class="tdCenter">
<asp:Label ID="TextTH9"  runat="server" Text="Label"></asp:Label>
</td>
</tr>
</table>
</div>