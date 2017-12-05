<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalModelInfo.ascx.cs" Inherits="Frame_APPModel_GlobalModelInfo" %>
        <script language="javascript" type="text/javascript">
            function txtShowNameEnter() {
                if (event.keyCode == 13 && event.srcElement.type != 'submit') {

                    var tempt = '<%=txtItemName.ClientID%>';
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
            function txtItemNameEnter() {
                if (event.keyCode == 13 && event.srcElement.type != 'submit') {

                    var tempt = '<%=ddlAppName.ClientID%>';
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
            function ddlAppNameEnter() {
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
            function txtItemDescriptionEnter() {
                if (event.keyCode == 13 && event.srcElement.type != 'submit') {

                    var tempt = '<%=txtShowName.ClientID%>';
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
        <td width="150px" class="tdStyleBorderBK">
         ShowName</td>
        <td>
         <asp:TextBox width="150px" ID="txtShowName" onkeydown="txtShowNameEnter()" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtShowName" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtShowName" ForeColor="Red" 
            ID="valeTxtItemName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>
            
       </td>
       </tr>

       <tr>
        <td class="tdStyleBorderBK" width="150px">ClassName</td>
        <td>
        <asp:TextBox ID="txtItemName" width="150px"  onkeydown="txtItemNameEnter()" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtShowName" runat="server" 
                    ErrorMessage="not null" ControlToValidate="txtItemName" ForeColor="Red"  
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" 
                ID="valeTxtShowName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30 ">
                </asp:RegularExpressionValidator>
        </td>
        </tr>

       <tr>
       <td class="tdStyleBorderBK" width="150px">
           AppType
        </td>
        <td>
            <asp:DropDownList ID="ddlAppName" width="155px" onkeydown="ddlAppNameEnter()" runat="server">
            </asp:DropDownList>
        </td>
        </tr>
       <tr>
            <td class="tdStyleBorderBK" width="150px">Description</td>
            <td>
             <asp:TextBox ID="txtItemDescription" width="150px" onkeydown="txtItemDescriptionEnter()"  runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td><asp:RegularExpressionValidator ControlToValidate="txtItemDescription" ForeColor="Red" 
            ID="valeTxtItemDescription" ValidationExpression="\S{0,50}" runat="server" ErrorMessage="字符长度0-50">
            </asp:RegularExpressionValidator>
        </td>
        </tr>
       
</table>