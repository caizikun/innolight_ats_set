<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopoEquipList.ascx.cs" Inherits="Frame_TestPlan_EquipList" %>
<%@ Register src="UPDownButton.ascx" tagname="UPDownButton" tagprefix="uc1" %>
<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
<tr>
 <th width="5px" ID="TH0Title" class="tHStyleLeft" runat="server" ></th>
    <th width="200px" ID="TH1Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH1" runat="server" Text="EquipName"></asp:Label>
    </th>
    <th width="150px"  ID="TH2Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH2" runat="server" Text="SEQ"></asp:Label>
    </th>
     <th width="150px" ID="TH3Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH3" runat="server" Text="Type"></asp:Label>
    </th>
     <th width="150px" ID="TH4Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH4" runat="server" Text="FuncType"></asp:Label>
    </th>
    <th width="200px" ID="TH5Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH5" runat="server" Text="Description"></asp:Label>
    </th>
    <th width="150px" ID="TH6Title" class="tHStyleLeft" runat="server" >Up/Down</th>
</tr>
<tr id="ContentTR" runat="server">
<td width="5px" style="border:solid #000 1px">
            <asp:CheckBox ID="chkIDEquip" runat="server" />    
        </td>
        <td width="200px" style="border:solid #000 1px">
            <asp:LinkButton ID="lnkItemName"
                runat="server">ItemName</asp:LinkButton> 
        </td>
        
        <td width="150px" style="border:solid #000 1px">
            <asp:Label ID="lblSeq" runat="server"></asp:Label>
        </td>

        <td width="150px" style="border:solid #000 1px">
            <asp:Label ID="txtItemType" runat="server"></asp:Label>
        </td>
        <td width="150px" style="border:solid #000 1px"> 
            <asp:Label ID="ddlRole" runat="server">
               </asp:Label>
        </td>
        <td width="200px" style="border:solid #000 1px">
            <asp:Label ID="txtItemDescription" runat="server"></asp:Label>
        </td>

        <td width="150px" style="border:solid #000 1px">
        <uc1:UPDownButton ID="UPDownSeqBtn" runat="server" EnableButtonDown="False" EnableButtonUP="False" />
        </td>
</tr>
</table>
</div>

        
