<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlowControlList.ascx.cs" Inherits="ASCXFlowControlList" %>

<div>
    <table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;table-layout:fixed; width:825px;" > 
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="100px"></td>
    <td width="100px"></td>
    <td width="100px"></td>
    <td width="100px"></td>
    <td width="100px"></td>
    <td width="100px"></td>
    </tr>
    <tr>
    <td width="25px" ID="TH0Title" class="tHStyleCenter" runat="server"></td>
    <td  width="200px" ID="TH1Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH1" runat="server" Text="Label"></asp:Label>
    </td>
     <td  width="100px" ID="TH3Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
    </td>
     <td  width="100px" ID="TH4Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
    </td>
    <td  width="100px" ID="TH5Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
    </td>
    <td  width="100px" ID="TH7Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH7" runat="server" Text="跳过否?"></asp:Label>
    </td>
     <td  width="100px" ID="TH6Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH6" runat="server" Text="测试模型"></asp:Label>
    </td>
    <td  width="100px" ID="TH2Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
    </td>
    </tr>    
    <tr id="ContentTR" runat="server" style=" background-color:White; height:30px">
    <td width="25px" class="tdCenter"><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td width="200px" class="tdLeft"><asp:LinkButton ID="LinkBItemName" runat="server" 
            onclick="LinkBItemName_Click">LinkButton</asp:LinkButton> </td>    
    <td width="100px" class="tdCenter"><asp:Label ID="LbChannel" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" class="tdCenter"><asp:Label ID="LbTemp" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" class="tdCenter"><asp:Label ID="LbVcc" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" class="tdCenter"><asp:Label ID="IgnorFlag" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" class="tdCenter"><asp:LinkButton ID="LinkBReviewTestMode" runat="server" 
            onclick="LinkBReviewTestMode_Click">查看</asp:LinkButton></td>
    <td width="100px" class="tdCenter"><asp:Label ID="LbSEQ" runat="server" Text="Label"></asp:Label></td>
    </tr>
    </table>
</div>

