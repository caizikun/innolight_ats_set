<%@ Control Language="C#" AutoEventWireup="true" CodeFile="roleInfo.ascx.cs" Inherits="Frame_UserRoleFunc_roleInfo" %>
        <script language="javascript" type="text/javascript">
            function txtRoleNameEnter() {
                if (event.keyCode == 13 && event.srcElement.type != 'submit') {
                    var tempt = '<%=txtRemarks.ClientID%>';
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
            function txtRemarksEnter() {
                if (event.keyCode == 13 && event.srcElement.type != 'submit') {
                    var tempt = '<%=txtRoleName.ClientID%>';
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
        <td width="150px" class="tdStyleBorderBK">角色名</td>
        <td>
         <asp:TextBox ID="txtRoleName" Width="200px" height="22px" onkeydown="txtRoleNameEnter()" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
            <asp:RequiredFieldValidator ID="valrTxtRoleName" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="txtRoleName" ForeColor="Red"  
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtRoleName" ForeColor="Red" Display="Dynamic"
            ID="valeTxtRoleName" ValidationExpression="\S{1,25}" runat="server" ErrorMessage="字符长度1-25">
            </asp:RegularExpressionValidator>
            
       </td>
       </tr>
       <tr>

       <td width="150px" class="tdStyleBorderBK">
         备注
        </td>
        <td>
            <asp:TextBox ID="txtRemarks" Width="200px" height="22px" onkeydown="txtRemarksEnter()"  runat="server"></asp:TextBox>            
        </td>
        <td>
        &nbsp;
        <asp:RegularExpressionValidator ControlToValidate="txtRemarks" ForeColor="Red" Display="Dynamic"
            ID="valeTxtRemarks" ValidationExpression="\S{0,25}" runat="server" ErrorMessage="字符长度0-25">
            </asp:RegularExpressionValidator>
        </td>
        </tr>
       
</table>