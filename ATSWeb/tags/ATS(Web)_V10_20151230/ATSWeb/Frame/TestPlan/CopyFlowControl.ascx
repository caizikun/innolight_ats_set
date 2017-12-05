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




