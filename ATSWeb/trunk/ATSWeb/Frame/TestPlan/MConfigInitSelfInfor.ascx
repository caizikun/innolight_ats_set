<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MConfigInitSelfInfor.ascx.cs" Inherits="ASCXMConfigInitSelfInfor" %>

<div>
<table cellspacing="1" cellpadding="0">
<tr>    
<td  width="150px" class="tdStyleBorderBK">
<asp:Label ID="FiledName" runat="server" Text="Label"></asp:Label>
</td>
<td >
    <asp:TextBox ID="FiledValue" width="200px" height="22px" runat="server" MaxLength="50"></asp:TextBox>
</td>
<td>
&nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ErrorMessage="不能为空" ControlToValidate="FiledValue" ForeColor="Red" 
        SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ErrorMessage="请输入0-65535之间的数字" 
        ControlToValidate="FiledValue" ForeColor="Red" MaximumValue="65535" Display="Dynamic"
        MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
        ErrorMessage="请输入字符串" ControlToValidate="FiledValue" Display="Dynamic"
        ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
</td>
</tr>
    
</table>
</div>