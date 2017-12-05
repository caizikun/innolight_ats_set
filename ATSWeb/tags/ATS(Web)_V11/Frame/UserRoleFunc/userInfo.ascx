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
<table style=" border:solid #000 1px">
        <tr>
        <td width="150px" class="tdStyleBorderBK">LoginName</td>
        <td>
         <asp:TextBox ID="txtLoginName" onkeydown="txtLoginNameEnter()" Width="200px" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtLoginName" runat="server" 
            ErrorMessage="not null" ControlToValidate="txtLoginName" ForeColor="Red"  
            SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtLoginName" ForeColor="Red" 
            ID="valeTxtLoginName" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>            
       </td>
       </tr>       

       <tr>
        <td width="150px" class="tdStyleBorderBK">Password</td>
        <td>
         <asp:TextBox ID="txtPwd"  onkeydown="txtPwdEnter()" Width="200px" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtPwd" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtPwd" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtPwd" ForeColor="Red" 
            ID="valeTxtPwd" ValidationExpression="\S{6,50}" runat="server" ErrorMessage="字符长度6-50">
            </asp:RegularExpressionValidator>
       </td>
       </tr>
       <tr>

       <td width="150px" class="tdStyleBorderBK">TrueName</td>
        <td>
         <asp:TextBox ID="txtTrueName" onkeydown="txtTrueNameEnter()" Width="200px" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RegularExpressionValidator ControlToValidate="txtTrueName" ForeColor="Red" 
            ID="valeTxtTrueName" ValidationExpression="\S{0,20}" runat="server" ErrorMessage="字符长度0-20">
            </asp:RegularExpressionValidator>
       </td>
        </tr>
       <tr>
       <td width="150px" class="tdStyleBorderBK">
         Remark
        </td>
        <td>
            <asp:TextBox ID="txtRemarks" onkeydown="txtRemarksEnter()" Width="200px" runat="server"></asp:TextBox>            
        </td>
        <td><asp:RegularExpressionValidator ControlToValidate="txtRemarks" ForeColor="Red" 
            ID="valeTxtRemarks" ValidationExpression="\S{0,50}" runat="server" ErrorMessage="字符长度0-50">
            </asp:RegularExpressionValidator>
        </td>
        </tr>
       
</table>