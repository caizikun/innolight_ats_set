<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalModelList.ascx.cs" Inherits="Frame_APPModel_GlobalModelList" %>
<div>
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
<tr>
   <th  ID="TH0Title" class="tHStyleLeft" width="5px" runat="server"></th>
   <th  ID="TH1Title" class="tHStyleLeft" width="200px" runat="server">
       <asp:Label ID="TH1" class="tdLeft" runat="server" Text="ModelName"></asp:Label>
   </th>    
   <th  ID="TH2Title" class="tHStyleLeft" width="150px" runat="server" >
       <asp:Label ID="TH2" class="tdLeft" runat="server" Text="AppType"></asp:Label>
   </th>
    
   <th  ID="TH3Title" class="tHStyleLeft" width="200px"  runat="server" >
       <asp:Label ID="TH3" class="tdLeft" runat="server" Text="Descriptions"></asp:Label>
   </th>
   <th ID="TH4Title" class="tHStyleLeft" width="150px" runat="server">
       <asp:Label ID="TH4" class="tdLeft" runat="server" Text="Params"></asp:Label>
   </th>
</tr>
<tr id="ContentTR" runat="server">
    <td width="5px" class="tdLeft" style="border:solid #000 1px">
            <asp:CheckBox ID="chkIDModel" runat="server" />    
        </td>

        <td width="200px" class="tdLeft" style="border:solid #000 1px">
            <asp:LinkButton ID="lnkItemName"
                runat="server">ShowName</asp:LinkButton>
            <asp:TextBox ID="txtItemName" runat="server" Visible="false"></asp:TextBox>
        </td>

       

        <td width="150px" class="tdLeft" style="border:solid #000 1px">
            <asp:Label ID="ddlAppName" runat="server"></asp:Label>
            <%-- 
            <asp:DropDownList ID="ddlAppName"
                runat="server">
            </asp:DropDownList>
            --%>
        </td>
        <td width="200px" class="tdLeft" style="border:solid #000 1px">
            <asp:TextBox ID="txtItemDescription" runat="server"></asp:TextBox>
            <asp:Label ID="lblItemDescription" runat="server"></asp:Label>
        </td>
        <td width="150px" class="tdLeft" style="border:solid #000 1px">
            <asp:LinkButton ID="lnkViewParams"
                runat="server"> View </asp:LinkButton>
        </td>
</tr>
</table>
</div>
        