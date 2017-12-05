<%@ Control Language="C#" AutoEventWireup="true" CodeFile="E2ROMSelinfor.ascx.cs" Inherits="ASCXE2ROMSelinfor" %>
<script language="javascript" type="text/javascript">
    function Colum2TextEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum2Text.ClientID%>';
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
<div>
<table cellspacing="1" cellpadding="0">
<tr>
   <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
   </td>
   <td>
    <asp:TextBox ID="Colum2Text" width="200px" height="22px" onkeydown="Colum2TextEnter()" runat="server"></asp:TextBox>
   </td>
   <td>
   &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
           ErrorMessage="不能为空" ControlToValidate="Colum2Text" ForeColor="Red" 
           SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="Colum2Text" ForeColor="Red" Display="Dynamic" 
            ID="RegularExpressionValidator1" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
       <asp:CompareValidator ID="CompareValidator1" runat="server" 
           ErrorMessage="请输入字符串" ControlToValidate="Colum2Text" Display="Dynamic"
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
</table>
</div>
