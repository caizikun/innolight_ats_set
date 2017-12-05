<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalSpecsInfo.ascx.cs" Inherits="Frame_GlobalSpecs_GlobalSpecsInfo" %>
        <script language="javascript" type="text/javascript">
            function txtItemNameEnter() {
                if (event.keyCode == 13 && event.srcElement.type != 'submit') {

                    var tempt = '<%=txtUnit.ClientID%>';
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
            function txtUnitEnter() {
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
        <td class="tdStyleBorderBK" width="150px">Item</td>
        <td>
         <asp:TextBox ID="txtItemName" width="150px" onkeydown="txtItemNameEnter()" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtItemName" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" 
            ID="valeTxtItemName" ValidationExpression="\S{1,100}" runat="server" ErrorMessage="字符长度1-100">
            </asp:RegularExpressionValidator>
            
       </td>
       </tr>
       <tr>
       <td class="tdStyleBorderBK" width="150px">Unit</td>
        <td>
         <asp:TextBox ID="txtUnit" width="150px" onkeydown="txtUnitEnter()" runat="server"></asp:TextBox>
        </td>
        <td>
            <%--<asp:RequiredFieldValidator ID="valrTxtUnit" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtUnit" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtUnit" ForeColor="Red" 
            ID="valeTxtUnit" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
             --%>
       </td>
        </tr>
       
        <tr>
            <td class="tdStyleBorderBK" width="150px">Description</td>
            <td>
             <asp:TextBox ID="txtItemDescription" width="150px" onkeydown="txtItemDescriptionEnter()"  runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
       
</table>