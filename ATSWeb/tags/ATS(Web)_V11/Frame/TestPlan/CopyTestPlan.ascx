<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CopyTestPlan.ascx.cs" Inherits="ASCXCopyTestPlan" %>

<style type="text/css">
    .style2
    {
        height: 10px;
    }
</style>

<script language="javascript" type="text/javascript">
    function TBItemNameEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {
            var tempt = '<%=TBItemName.ClientID%>';
            var tempid = document.getElementById(tempt);
            if (tempid == null) {
                return false;
            }
            else {
                tempid.focus();
                return false;
            }


        }
    }
</script>
<table style=" border:solid #000 1px">
    <tr> 
        <td width="100px" class="tdStyleBorderBK">
            <asp:Label ID="TH2" runat="server" Text="NewName"></asp:Label>      
        </td> 
        <td width="130px"  ><asp:TextBox ID="TBItemName" onkeydown="TBItemNameEnter()" runat="server" MaxLength="30"></asp:TextBox></td>
        <td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="not null" ControlToValidate="TBItemName" ForeColor="Red" 
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ErrorMessage="please input a string" ControlToValidate="TBItemName" 
                Operator="NotEqual" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator>
        </td>
        
    </tr>
</table>

<table>
<tr>
<td class="style2">
</td>
</tr>
<tr>
<td >
    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
        oncheckedchanged="CheckBox1_CheckedChanged" />
</td>
<td>    
    <asp:Label ID="Label1" runat="server" Text="Copy Plan to other PN"></asp:Label>    
</td>
</tr>
</table>

<asp:Panel ID="Panel1" runat="server" Height="59px" Width="281px" 
    Visible="False">
<table style=" border:solid #000 1px; width: 451px;">
<tr>
 <td width="100px" class="tdStyleBorderBK">
    <asp:Label ID="Label3" runat="server" Text="Type"></asp:Label>
</td>
<td>    
    <asp:DropDownList ID="DropDownList1" runat="server" Height="22px" Width="154px" 
        AutoPostBack="True" ontextchanged="DropDownList1_TextChanged" 
        >
    </asp:DropDownList>    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="DropDownList1" ErrorMessage="not null" ForeColor="Red" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
 <td width="100px" class="tdStyleBorderBK">
    <asp:Label ID="Label4" runat="server" Text="PN"></asp:Label>   
</td>
<td>   
    <asp:DropDownList ID="DropDownList2" runat="server" Height="22px" Width="154px">
    </asp:DropDownList>   
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="DropDownList2" ErrorMessage="not null" ForeColor="Red" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
</tr>
</table>
</asp:Panel>


