<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CopyMCoef.ascx.cs" Inherits="ASCXCopyMCoef" %>

<script language="javascript" type="text/javascript">
    function TBItemNameEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {
            var tempt = '<%=txtItemDescription.ClientID%>';
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
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                ErrorMessage="不能为空" ControlToValidate="TBItemName" ForeColor="Red" 
                SetFocusOnError="True"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="TBItemName" ForeColor="Red" Display="Dynamic"
            ID="RegularExpressionValidator3" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ErrorMessage="请输入字符串" ControlToValidate="TBItemName" Display="Dynamic"
                Operator="NotEqual" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator>
        </td>
        
    </tr>

    <tr>
        <td width="150px" class="tdStyleBorderBK1">描述</td>
        <td>
            <asp:TextBox ID="txtItemDescription" width="198px" height="48px" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
        </td>
        <td>
        &nbsp;
        <asp:RegularExpressionValidator ControlToValidate="txtItemDescription" ForeColor="Red" Display="Dynamic"
        ID="valeTxtItemDescription" ValidationExpression="\S{0,200}" runat="server" ErrorMessage="字符长度0-200">
        </asp:RegularExpressionValidator>
    </td>
    </tr>     
</table>




