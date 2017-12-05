<%@ Control Language="C#" AutoEventWireup="true" CodeFile="userInfo.ascx.cs" Inherits="Frame_UserRoleFunc_userInfo" %>
        <script language="javascript" type="text/javascript">

            function txtLoginNameEnter() {
                if (event.keyCode == 13 && event.srcElement.type != 'submit') {
                    var tempt = '<%=txtPwd.ClientID%>';
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
            function txtPwdEnter() {
                if (event.keyCode == 13 && event.srcElement.type != 'submit') {
                    var tempt = '<%=txtTrueName.ClientID%>';
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
            function txtTrueNameEnter() {
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
                    var tempt = '<%=txtLoginName.ClientID%>';
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
        <td width="150px" class="tdStyleBorderBK">登录名</td>
        <td>
         <asp:TextBox ID="txtLoginName" onkeydown="txtLoginNameEnter()" Width="200px" height="22px" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
            <asp:RequiredFieldValidator ID="valrTxtLoginName" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="txtLoginName" ForeColor="Red"  
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtLoginName" ForeColor="Red" Display="Dynamic"
            ID="valeTxtLoginName" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>            
       </td>
       </tr>       

       <tr>
        <td width="150px" class="tdStyleBorderBK">密码</td>
        <td>
         <asp:TextBox ID="txtPwd"  onkeydown="txtPwdEnter()" Width="200px" height="22px" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
            <asp:RequiredFieldValidator ID="valrTxtPwd" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtPwd" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtPwd" ForeColor="Red" Display="Dynamic"
            ID="valeTxtPwd" ValidationExpression="\S{6,50}" runat="server" ErrorMessage="字符长度6-50">
            </asp:RegularExpressionValidator>
       </td>
       </tr>
       <tr>

       <td width="150px" class="tdStyleBorderBK">名字</td>
        <td>
         <asp:TextBox ID="txtTrueName" onkeydown="txtTrueNameEnter()" Width="200px" height="22px" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
            <asp:RegularExpressionValidator ControlToValidate="txtTrueName" ForeColor="Red" Display="Dynamic"
            ID="valeTxtTrueName" ValidationExpression="\S{0,20}" runat="server" ErrorMessage="字符长度0-20">
            </asp:RegularExpressionValidator>
       </td>
        </tr>
       <tr>
       <td width="150px" class="tdStyleBorderBK">
         备注
        </td>
        <td>
            <asp:TextBox ID="txtRemarks" onkeydown="txtRemarksEnter()" Width="200px" height="22px" runat="server"></asp:TextBox>            
        </td>
        <td>
        &nbsp;
        <asp:RegularExpressionValidator ControlToValidate="txtRemarks" ForeColor="Red" Display="Dynamic"
            ID="valeTxtRemarks" ValidationExpression="\S{0,50}" runat="server" ErrorMessage="字符长度0-50">
            </asp:RegularExpressionValidator>
        </td>
        </tr>
       
</table>