<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestPlanList.ascx.cs" Inherits="ASCXTestPlanList" %>
<div  >
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
<tr>
   <th ID="TH0Title" class="tHStyleLeft" width="5px" runat="server"></th>
   <th ID="TH1Title" class="tHStyleLeft" width="136px"  runat="server">
       <asp:Label ID="TH1" class="tdLeft" runat="server" Text="Label"></asp:Label>
   </th>
   <th ID="TH2Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH2" class="tdLeft" runat="server" Text="Label"></asp:Label>
   </th>
   <th ID="TH3Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH3" class="tdLeft" runat="server" Text="Label"></asp:Label>
   </th>
    <th ID="TH8Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH8" class="tdLeft" runat="server" Text="Label"></asp:Label>
   </th>
   <th ID="TH4Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH4" class="tdLeft" runat="server" Text="Label"></asp:Label>
   </th>
   <th ID="TH5Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH5" class="tdLeft" runat="server" Text="Flowcontrol"></asp:Label>
   </th>
   <th ID="TH6Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH6" class="tdLeft" runat="server" Text="Equipment"></asp:Label>
   </th>
   <th ID="TH7Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH7" class="tdLeft" runat="server" Text="Mconfiginit"></asp:Label>
   </th>
   <th ID="TH9Title" class="tHStyleLeft" width="136px" runat="server">
       <asp:Label ID="TH9" class="tdLeft" runat="server" Text="TopPNSpecs"></asp:Label>
   </th>
</tr>
<tr id="ContentTR" runat="server">
<td width="5px" class="tdLeft" style="border:solid #000 1px">
<asp:CheckBox ID="IsSelected" runat="server" AutoPostBack="false"  />
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:LinkButton ID="LBTestplanSelf"  runat="server" onclick="LBTestplanSelf_Click">LinkButton</asp:LinkButton>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="LabelSWVer"  runat="server" Text="Label"></asp:Label>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="LabelHWVer"  runat="server" Text="Label"></asp:Label>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:Label ID="LabelVersion"  runat="server" Text="Label"></asp:Label>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<div id="AllDescription" runat="server" class="hideText" title="">
<asp:Label ID="LBDescription" runat="server" Text=""></asp:Label>
</div>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:LinkButton ID="LBFlowControl"  runat="server" onclick="LBFlowControl_Click">View</asp:LinkButton>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:LinkButton ID="LBEquipment"  runat="server" onclick="LBEquipment_Click">View</asp:LinkButton>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:LinkButton ID="LBMconfiginit"  runat="server" onclick="LBMconfiginit_Click">View</asp:LinkButton>
</td>
<td width="136px" class="tdLeft" style="border:solid #000 1px">
<asp:LinkButton ID="LBTopPNSpecs"  runat="server" >View</asp:LinkButton>
</td>
</tr>
</table>
</div>