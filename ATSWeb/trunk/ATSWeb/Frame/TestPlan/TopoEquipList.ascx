<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopoEquipList.ascx.cs" Inherits="Frame_TestPlan_EquipList" %>

<div>
<table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:775px;">
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="150px"></td>
    <td width="100px"></td>
    <td width="200px"></td>
    <td width="100px"></td>
    </tr>
<tr>
 <td width="25px" ID="TH0Title" class="tHStyleCenter" runat="server" ></td>
    <td width="200px" ID="TH1Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
    </td>
     <td width="150px" ID="TH3Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH3" runat="server" Text="类型"></asp:Label>
    </td>
     <td width="100px" ID="TH4Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH4" runat="server" Text="功能类型"></asp:Label>
    </td>
    <td width="200px" ID="TH5Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH5" runat="server" Text="描述"></asp:Label>
    </td>
    <td width="100px"  ID="TH2Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH2" runat="server" Text="排序"></asp:Label>
    </td>
</tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
<td width="25px" class="tdCenter">
            <asp:CheckBox ID="chkIDEquip" runat="server" />    
        </td>
        <td width="200px" class="tdLeft">
            <asp:LinkButton ID="lnkItemName"
                runat="server">ItemName</asp:LinkButton> 
        </td>
        <td width="150px" class="tdLeft">
            <asp:Label ID="txtItemType" runat="server"></asp:Label>
        </td>
        <td width="100px" class="tdCenter"> 
            <asp:Label ID="ddlRole" runat="server">
               </asp:Label>
        </td>
        <td width="200px" class="tdLeft">
            <asp:Label ID="txtItemDescription" runat="server"></asp:Label>
        </td>               
        <td width="100px" class="tdCenter">
            <asp:Label ID="lblSeq" runat="server"></asp:Label>
        </td>
</tr>
</table>
</div>

        
