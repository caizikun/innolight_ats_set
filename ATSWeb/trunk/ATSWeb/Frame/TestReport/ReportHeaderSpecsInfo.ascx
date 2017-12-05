<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportHeaderSpecsInfo.ascx.cs" Inherits="Frame_ReportHeaderSpecsInfo" %>

<table cellspacing="1" cellpadding="0">
        <tr>
        <td class="tdStyleBorderBK" width="150px">名称</td>
        <td>
              <asp:DropDownList ID="Colum1Text" width="204px" height="28px" runat="server">
            </asp:DropDownList>
        </td>
       </tr>   
        <tr>
        <td class="tdStyleBorderBK" width="150px">显示别名</td>
        <td>
         <asp:TextBox ID="txtShowName" width="200px" height="22px" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
            <asp:RegularExpressionValidator ControlToValidate="txtShowName" ForeColor="Red" Display="Dynamic" 
            ID="valeTxtShowName" ValidationExpression="\S{0,150}" runat="server" ErrorMessage="字符长度0-150">
            </asp:RegularExpressionValidator>
            
       </td>
       </tr>         
</table>

    &nbsp;
    <asp:Label ID="Label1" runat="server" Text="当前报表表头已包含所有规格项！" 
        style="display:none" ></asp:Label>
       