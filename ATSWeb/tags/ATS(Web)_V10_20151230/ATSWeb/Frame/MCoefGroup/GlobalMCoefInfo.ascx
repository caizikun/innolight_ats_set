<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalMCoefInfo.ascx.cs" Inherits="Frame_MCoefGroup_GlobalMCoefInfo" %>
         <script language="javascript" type="text/javascript">
             function txtItemNameEnter() {
                 if (event.keyCode == 13 && event.srcElement.type != 'submit') {

                     var tempt = '<%=ddlTypeName.ClientID%>';
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
             function ddlTypeNameEnter() {
                 if (event.keyCode == 13 && event.srcElement.type != 'submit') {

                     var tempt = '<%=ddlIgnoreFlag.ClientID%>';
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
             function ddlIgnoreFlagEnter() {
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
         </script>
<table style=" border:solid #000 1px">
        <tr>
        <td width="150px" class="tdStyleBorderBK">Item</td>
        <td>
         <asp:TextBox ID="txtItemName" Width="150px" onkeydown="txtItemNameEnter()" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtItemName" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" 
            ID="valeTxtItemName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>
       </td>
       </tr>
       <tr>
       <td width="150px" class="tdStyleBorderBK">
           Type
        </td>
        <td>
            <asp:DropDownList ID="ddlTypeName" Width="155px" onkeydown="ddlTypeNameEnter()" runat="server">
            </asp:DropDownList>
        </td>
        </tr>
        <tr>
            <td width="150px" class="tdStyleBorderBK">IgnoreFlag</td>
            <td><asp:DropDownList ID="ddlIgnoreFlag" Width="155px" onkeydown="ddlIgnoreFlagEnter()" runat="server">
                <asp:ListItem>true</asp:ListItem>
                <asp:listitem>false</asp:listitem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td width="150px" class="tdStyleBorderBK">Description</td>
            <td>
             <asp:TextBox ID="txtItemDescription" onkeydown="txtItemDescriptionEnter()" Width="150px" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td><asp:RegularExpressionValidator ControlToValidate="txtItemDescription" ForeColor="Red" 
            ID="valeTxtItemDescription" ValidationExpression="\S{0,200}" runat="server" ErrorMessage="字符长度0-200">
            </asp:RegularExpressionValidator>
        </td>
        </tr>     
</table>