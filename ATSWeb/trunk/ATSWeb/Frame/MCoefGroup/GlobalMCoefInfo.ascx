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
<table cellspacing="1" cellpadding="0">
        <tr>
        <td width="150px" class="tdStyleBorderBK">名称</td>
        <td>
         <asp:TextBox ID="txtItemName" width="200px" height="22px" onkeydown="txtItemNameEnter()" runat="server"></asp:TextBox>
        </td>
        <td>
         &nbsp;
            <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtItemName" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" Display="Dynamic"
            ID="valeTxtItemName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>
       </td>
       </tr>
       <tr>
       <td width="150px" class="tdStyleBorderBK">
           模块类型
        </td>
        <td>
            <asp:DropDownList ID="ddlTypeName" width="204px" height="28px" onkeydown="ddlTypeNameEnter()" runat="server">
            </asp:DropDownList>
        </td>
        </tr>
        <tr>
            <td width="150px" class="tdStyleBorderBK">是否忽略</td>
            <td><asp:DropDownList ID="ddlIgnoreFlag" width="204px" height="28px" onkeydown="ddlIgnoreFlagEnter()" runat="server">
                <asp:ListItem>true</asp:ListItem>
                <asp:listitem>false</asp:listitem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td width="150px" class="tdStyleBorderBK1">描述</td>
            <td>
             <asp:TextBox ID="txtItemDescription" onkeydown="txtItemDescriptionEnter()" width="198px" height="48px" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
             &nbsp;
             <asp:RegularExpressionValidator ControlToValidate="txtItemDescription" ForeColor="Red" Display="Dynamic"
            ID="valeTxtItemDescription" ValidationExpression="\S{0,200}" runat="server" ErrorMessage="字符长度0-200">
            </asp:RegularExpressionValidator>
        </td>
        </tr>     
</table>