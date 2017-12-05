<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalModelParamsList.ascx.cs" Inherits="Frame_APPModel_GlobalModelParamsList" %>
<div>
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
<tr>
  <th ID="TH0Title" class="tHStyleLeft" width="5px" runat="server"></th>
   <th ID="TH1Title" class="tHStyleLeft" runat="server">
       <asp:Label ID="TH1" class="tdLeft" runat="server" Text="Item"></asp:Label>
   </th>
   <th ID="TH5Title" class="tHStyleLeft" runat="server">
       <asp:Label ID="TH5" class="tdLeft" runat="server" Text="ShowName"></asp:Label>
   </th>
   <th ID="TH2Title" class="tHStyleLeft" width="100px" runat="server">
       <asp:Label ID="TH2" class="tdLeft" runat="server" Text="ValueType"></asp:Label>
   </th>   
   <th ID="TH3Title" class="tHStyleLeft" width="200px" runat="server">
       <asp:Label ID="TH3" class="tdLeft" runat="server" Text="Value"></asp:Label>
   </th>
   <%--
   <th class="tdLeft">
       <asp:Label ID="TH3" class="tdLeft" runat="server" Text="Direction"></asp:Label>
   </th>
    <th class="tdLeft">
       <asp:Label ID="TH5" class="tdLeft" runat="server" Text="SpecMin"></asp:Label>
   </th>
    <th class="tdLeft">
       <asp:Label ID="TH6" class="tdLeft" runat="server" Text="SpecMax"></asp:Label>
   </th>
    <th class="tdLeft">
       <asp:Label ID="TH7" class="tdLeft" runat="server" Text="LogRecord"></asp:Label>
   </th>
    <th class="tdLeft">
       <asp:Label ID="TH8" class="tdLeft" runat="server" Text="Specific?"></asp:Label>
   </th>
    <th class="tdLeft">
       <asp:Label ID="TH9" class="tdLeft" runat="server" Text="DataRecord?"></asp:Label>
   </th>
   --%>
    <th ID="TH4Title" class="tHStyleLeft" width="200px" runat="server">
       <asp:Label ID="TH4" class="tdLeft" runat="server" Text="Description"></asp:Label>
   </th>
</tr>
<tr id="ContentTR" runat="server">
<td width="5px" class="tdLeft" style="border:solid #000 1px">
    <asp:CheckBox ID="chkIDModelParam" runat="server" />
    </td>
    <td width="200px" class="tdLeft" style="border:solid #000 1px">
    <asp:LinkButton ID="lnkItemName"
        runat="server">ItemName</asp:LinkButton>     
        </td>

    <td width="200px" class="tdLeft" style="border:solid #000 1px">
    <asp:Label ID="txtShowName" runat="server"  Enabled="False" >ShowName</asp:Label>    
    </td>
    <td width="100px" class="tdLeft" style="border:solid #000 1px">
    <asp:Label ID="txtItemType" runat="server"  Enabled="False" >ItemType</asp:Label>    
    </td>
    
    <td width="200px" class="tdLeft" style="border:solid #000 1px">         
    <asp:Label ID="txtItemValue" runat="server"  >ItemValue</asp:Label>       
    </td>
    <%--
    <td width="100px" class="tdLeft" style="border:solid #000 1px">    
    <asp:Label ID="txtDirection" runat="server" Enabled="False" >ItemDirect</asp:Label>
    </td>
    <td width="100px" class="tdLeft" style="border:solid #000 1px">    
    <asp:Label ID="txtSpecMin" runat="server"></asp:Label> 
    </td>
    <td width="100px" class="tdLeft" style="border:solid #000 1px">    
    <asp:Label ID="txtSpecMax" runat="server"></asp:Label>
    </td>
    <td width="100px" class="tdLeft" style="border:solid #000 1px">
    <asp:Label ID="ddlItemSpecific" runat="server"></asp:Label>    
    <asp:DropDownList ID="ddlItemSpecific" runat="server">
        <asp:ListItem>0</asp:ListItem>
        <asp:ListItem>1</asp:ListItem>
    </asp:DropDownList>    
    </td>
    <td width="100px" class="tdLeft" style="border:solid #000 1px">
    <asp:Label ID="ddlLogRecord" runat="server"></asp:Label>    
    <asp:DropDownList ID="ddlLogRecord" runat="server">
        <asp:ListItem>0</asp:ListItem>
        <asp:ListItem>1</asp:ListItem>
    </asp:DropDownList>    
    </td>
    不再存在该字段
    <td>
    <asp:Label ID="ddlFailBreak" runat="server"></asp:Label>    
    <asp:DropDownList ID="ddlFailBreak" runat="server">
        <asp:ListItem>0</asp:ListItem>
        <asp:ListItem>1</asp:ListItem>
    </asp:DropDownList>    
    </td>    
    <td width="100px" class="tdLeft" style="border:solid #000 1px">
    <asp:Label ID="ddlDataRecord" runat="server"></asp:Label>
    
    <asp:DropDownList ID="ddlDataRecord" runat="server">
        <asp:ListItem>0</asp:ListItem>
        <asp:ListItem>1</asp:ListItem>
    </asp:DropDownList>    
    </td>
    --%>
    <td width="200px" class="tdLeft" style="border:solid #000 1px">
        <asp:Label ID="txtItemDescription" runat="server"  ></asp:Label>
    </td>
</tr>
</table>
</div>
    
    
