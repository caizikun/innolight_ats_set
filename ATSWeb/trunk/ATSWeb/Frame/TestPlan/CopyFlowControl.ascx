<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CopyFlowControl.ascx.cs" Inherits="ASCXCopyFlowControl" %>

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
<table cellspacing="1" cellpadding="0">
    <tr> 
        <td width="150px" class="tdStyleBorderBK">
            <asp:Label ID="TH2" runat="server" Text="名称"></asp:Label>      
        </td> 
        <td><asp:TextBox ID="TBItemName" width="200px" height="22px" onkeydown="TBItemNameEnter()" runat="server" MaxLength="30"></asp:TextBox></td>
        <td>
        &nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="TBItemName" ForeColor="Red" 
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="TBItemName" ForeColor="Red" Display="Dynamic"
            ID="RegularExpressionValidator3" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ErrorMessage="请输入字符串" ControlToValidate="TBItemName" Display="Dynamic"
                Operator="NotEqual" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator>
        </td>
        
    </tr>
</table>




