<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalModelParamsList.ascx.cs" Inherits="Frame_APPModel_GlobalModelParamsList" %>
<div>
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;table-layout:fixed; width:925px;">
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="200px"></td>
    <td width="100px"></td>        
    <td width="150px"></td>
    <td width="250px"></td>
    </tr>
<tr>
  <td ID="TH0Title" class="tHStyleCenter" width="25px" runat="server"></td>
   <td ID="TH1Title" class="tHStyleCenter" width="200px" runat="server">
       <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
   </td>
   <td ID="TH5Title" class="tHStyleCenter" width="200px" runat="server">
       <asp:Label ID="TH5" runat="server" Text="别名"></asp:Label>
   </td>
   <td ID="TH2Title" class="tHStyleCenter" width="100px" runat="server">
       <asp:Label ID="TH2" runat="server" Text="参数类型"></asp:Label>
   </td>   
   <td ID="TH3Title" class="tHStyleCenter" width="150px" runat="server">
       <asp:Label ID="TH3"  runat="server" Text="值"></asp:Label>
   </td>
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
    <td ID="TH4Title" class="tHStyleCenter" width="250px" runat="server">
       <asp:Label ID="TH4" runat="server" Text="描述"></asp:Label>
   </td>
</tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
<td width="25px" class="tdCenter">
    <asp:CheckBox ID="chkIDModelParam" runat="server" />
    </td>
    <td width="200px" class="tdLeft">
    <asp:LinkButton ID="lnkItemName"
        runat="server">ItemName</asp:LinkButton>     
        </td>

    <td width="200px" class="tdLeft">
    <asp:Label ID="txtShowName" runat="server"  Enabled="False" >ShowName</asp:Label>    
    </td>
    <td width="100px" class="tdLeft">
    <asp:Label ID="txtItemType" runat="server"  Enabled="False" >ItemType</asp:Label>    
    </td>
    
    <td width="150px" class="tdLeft">         
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
    <td width="250px" class="tdLeft">
        <asp:Label ID="txtItemDescription" runat="server"  ></asp:Label>
    </td>
</tr>
</table>
</div>
    
    
