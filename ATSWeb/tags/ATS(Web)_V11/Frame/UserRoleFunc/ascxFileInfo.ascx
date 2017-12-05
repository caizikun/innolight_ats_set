<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ascxFileInfo.ascx.cs" Inherits="Frame_UserRoleFunc_ascxFileInfo" %>
        <script language="javascript" type="text/javascript">
            function txtAscxFileNameEnter() {
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
                    var tempt = '<%=txtAscxFileName.ClientID%>';
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
        <td width="150px" class="tdStyleBorderBK">AscxFileName</td>
        <td>
         <asp:TextBox ID="txtAscxFileName" Width="200px" onkeydown="txtAscxFileNameEnter()" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtAscxFileName" runat="server" 
           ErrorMessage="not null" ControlToValidate="txtAscxFileName" ForeColor="Red" 
           SetFocusOnError="True"></asp:RequiredFieldValidator>
       </td>
       </tr>
       <tr>

       <td width="150px" class="tdStyleBorderBK">
         Remarks
        </td>
        <td>
            <asp:TextBox ID="txtRemarks" Width="200px" onkeydown="txtRemarksEnter()"  runat="server"></asp:TextBox>            
        </td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtRemarks" runat="server" 
           ErrorMessage="not null" ControlToValidate="txtRemarks" ForeColor="Red" 
           SetFocusOnError="True"></asp:RequiredFieldValidator>
       </td>
        </tr>
       
</table>