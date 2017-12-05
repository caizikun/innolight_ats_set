<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FormulaList.ascx.cs" Inherits="ASCX_Chip_FormulaList" %>
<div  >
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
<tr>
   <th ID="TH0Title" class="tHStyleLeft" width="5px" runat="server"></th>
   <th ID="TH1Title" class="tHStyleLeft" width="136px"  runat="server">
       <asp:Label ID="TH1" class="tdLeft" runat="server" Text="Name"></asp:Label>
   </th>
   <th ID="TH2Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH2" class="tdLeft" runat="server" Text="WriteFormula"></asp:Label>
   </th>
   <th ID="TH3Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH3" class="tdLeft" runat="server" Text="AnalogueUnit"></asp:Label>
   </th>
    <th ID="TH4Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH4" class="tdLeft" runat="server" Text="ReadFormula"></asp:Label>
   </th>
     <th ID="TH5Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH5" class="tdLeft" runat="server" Text="Address(Dec)"></asp:Label>
   </th>
    <th ID="TH6Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH6" class="tdLeft" runat="server" Text="StartBit"></asp:Label>
   </th>
  <th ID="TH7" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH7Title" class="tdLeft" runat="server" Text="EndBit"></asp:Label>
   </th>
   <th ID="TH8Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH8" class="tdLeft" runat="server" Text="UnitLength"></asp:Label>
   </th>
  <th ID="TH9Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH9" class="tdLeft" runat="server" Text="ChipLine"></asp:Label>
   </th>
  
</tr>
<tr id="ContentTR" runat="server">
<td width="5px" class="tdLeft" style="border:solid #000 1px">
<asp:CheckBox ID="IsSelected" runat="server" AutoPostBack="false"  />
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:LinkButton ID="TextTH1"  runat="server" >LinkButton</asp:LinkButton>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="TextTH2"  runat="server" Text="Label"></asp:Label>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="TextTH3"  runat="server" Text="Label"></asp:Label>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="TextTH4"  runat="server" Text="Label"></asp:Label>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="TextTH5"  runat="server" Text="Label"></asp:Label>

</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="TextTH6"  runat="server" Text="Label"></asp:Label>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="TextTH7"  runat="server" Text="Label"></asp:Label>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="TextTH8"  runat="server" Text="Label"></asp:Label>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="TextTH9"  runat="server" Text="Label"></asp:Label>
</td>
</tr>
</table>
</div>