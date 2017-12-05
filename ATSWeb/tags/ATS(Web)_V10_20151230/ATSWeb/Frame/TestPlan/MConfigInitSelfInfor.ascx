<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MConfigInitSelfInfor.ascx.cs" Inherits="ASCXMConfigInitSelfInfor" %>

<div>
<table style=" border:solid #000 1px">
<tr>
    
<td  width="150px" class="tdStyleBorderBK">
<asp:Label ID="FiledName" runat="server" Text="Label"></asp:Label>
</td>
<td >
    <asp:TextBox ID="FiledValue" Width="200px" runat="server"></asp:TextBox>
</td>
<td>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ErrorMessage="not null" ControlToValidate="FiledValue" ForeColor="Red" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ErrorMessage="please input a number between 0-32767" 
        ControlToValidate="FiledValue" ForeColor="Red" MaximumValue="32767" 
        MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
</td>
</tr>
    
</table>
</div>