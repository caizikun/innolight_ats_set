<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlowControlList.ascx.cs" Inherits="ASCXFlowControlList" %>
<%@ Register src="UPDownButton.ascx" tagname="UPDownButton" tagprefix="uc1" %>
<div>
    <table cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;" > 
    <tr>
    <th width="5px" ID="TH0Title" class="tHStyleLeft" runat="server"></th>
    <th  width="120px" ID="TH1Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH1" runat="server" Text="Label"></asp:Label>
    </th>
    <th  width="100px" ID="TH2Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
    </th>
     <th  width="100px" ID="TH3Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
    </th>
     <th  width="100px" ID="TH4Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
    </th>
    <th  width="100px" ID="TH5Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
    </th>
    <th  width="100px" ID="TH7Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH7" runat="server" Text="Skip?"></asp:Label>
    </th>
     <th  width="100px" ID="TH6Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH6" runat="server" Text="TestMode"></asp:Label>
    </th>
    <th  width="100px" ID="TH8Title" class="tHStyleLeft" runat="server">UP/Down</th>
    </tr>    
    <tr id="ContentTR" runat="server">
    <td width="5px" style="border:solid #000 1px"><asp:CheckBox ID="IsSelected" runat="server" /></td>        
    <td width="120px" style="border:solid #000 1px"><asp:LinkButton ID="LinkBItemName" runat="server" 
            onclick="LinkBItemName_Click">LinkButton</asp:LinkButton> </td>    
    <td width="100px" style="border:solid #000 1px"><asp:Label ID="LbSEQ" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" style="border:solid #000 1px"><asp:Label ID="LbChannel" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" style="border:solid #000 1px"><asp:Label ID="LbTemp" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" style="border:solid #000 1px"><asp:Label ID="LbVcc" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" style="border:solid #000 1px"><asp:Label ID="IgnorFlag" runat="server" Text="Label"></asp:Label></td>
    <td width="100px" style="border:solid #000 1px"><asp:LinkButton ID="LinkBReviewTestMode" runat="server" 
            onclick="LinkBReviewTestMode_Click">View</asp:LinkButton></td>
    <td width="100px" style="border:solid #000 1px">
        <uc1:UPDownButton ID="UPDownButton1" runat="server" />
        </td>
    </tr>
    </table>
</div>

