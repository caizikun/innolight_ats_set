<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalEquipParam.ascx.cs" Inherits="Frame_Equipment_GlobalEquipParam" %>
     
     <script language="javascript" type="text/javascript">
         function txtItemNameEnter() {
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
            function txtShowNameEnter() {
                if (event.keyCode == 13 && event.srcElement.type != 'submit') {

                    var tempt = '<%=txtItemTypeDDList.ClientID%>';
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
            function txtItemTypeEnter() {
                if (event.keyCode == 13 && event.srcElement.type != 'submit') {

                    var tempt = '<%=txtItemValue.ClientID%>';
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
            function txtItemValueEnter() {
                if (event.keyCode == 13 && event.srcElement.type != 'submit') {

                    var tempt = '<%=ddlNeedCheckParams.ClientID%>';
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
            function ddlNeedCheckParamsEnter() {
                if (event.keyCode == 13 && event.srcElement.type != 'submit') {

                    var tempt = '<%=txtOptionalparams.ClientID%>';
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
            function txtOptionalparamsEnter() {
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
    <asp:TextBox ID="txtItemName" width="150px" onkeydown="txtItemNameEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
        <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtItemName" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" 
            ID="valeTxtItemName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30 ">
            </asp:RegularExpressionValidator>
    </td>
    </tr>
    <tr>
    <td width="150px" class="tdStyleBorderBK">ShowName</td>
    <td>
    <asp:TextBox ID="txtShowName" width="150px" onkeydown="txtShowNameEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
        <asp:RequiredFieldValidator ID="valrTxtShowName" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtShowName" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="txtShowName" ForeColor="Red" 
            ID="valeTxtShowName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30 ">
            </asp:RegularExpressionValidator>
    </td>
    </tr>
    <tr>
    <td width="150px" class="tdStyleBorderBK">Type</td>
    <td>
    <asp:DropDownList ID="txtItemTypeDDList" Width="155px" onkeydown="txtItemTypeEnter()" AutoPostBack="true" runat="server" 
                onselectedindexchanged="txtItemTypeDDList_SelectedIndexChanged">            
            <asp:ListItem>string</asp:ListItem>
            <asp:ListItem>byte</asp:ListItem>
            <asp:ListItem>bool</asp:ListItem>
             <asp:ListItem>double</asp:ListItem>  
             <asp:ListItem>int</asp:ListItem>
             <asp:ListItem>ArrayList</asp:ListItem>
            </asp:DropDownList>   
    </td>
    </tr>
    <tr>
    <td width="150px" class="tdStyleBorderBK">Value</td>
    <td>
    <asp:TextBox ID="txtItemValue" width="150px" onkeydown="txtItemValueEnter()" 
            runat="server" MaxLength="255"></asp:TextBox>
    </td>
    <td>
        <asp:RequiredFieldValidator ID="valrTxtItemValue" runat="server" ErrorMessage="not null" ControlToValidate="txtItemValue" ForeColor="Red"  
               ></asp:RequiredFieldValidator>
        
        <asp:RangeValidator ID="valcTxtItemValue"  runat="server"  ControlToValidate="txtItemValue" ErrorMessage="please input a correct number"
           ForeColor="Red"  ></asp:RangeValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidatorItemValue" 
            runat="server" ControlToValidate="txtItemValue" 
            ErrorMessage="" ForeColor="Red"></asp:RegularExpressionValidator>
    </td>
    </tr>

    <tr>
    <td width="150px" class="tdStyleBorderBK">NeedSelect</td>
    <td>
        <asp:DropDownList ID="ddlNeedCheckParams" width="155px" onkeydown="ddlNeedCheckParamsEnter()" runat="server">
        <asp:ListItem>false</asp:ListItem>
        <asp:ListItem>true</asp:ListItem>        
        </asp:DropDownList>    
    </td>
    <td>
        
    </td>
    </tr>

    <tr>
    <td width="150px" class="tdStyleBorderBK">Optionalparams</td>
    <td>
    <asp:TextBox ID="txtOptionalparams" width="150px" onkeydown="txtOptionalparamsEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
        <%--<asp:RequiredFieldValidator ID="valrTxtOptionalparams" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtOptionalparams" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="txtOptionalparams" ForeColor="Red" 
            ID="valeTxtOptionalparams" ValidationExpression="\S{1,4000}" runat="server" ErrorMessage="字符长度1-4000">
        </asp:RegularExpressionValidator>
         --%>        
    </td>
    </tr>
   <tr> 
        <td width="150px" class="tdStyleBorderBK">ItemDescription</td> 
        <td><asp:TextBox ID="txtItemDescription" width="150px" 
                onkeydown="txtItemDescriptionEnter()" runat="server" Rows="3" 
                TextMode="MultiLine" MaxLength="500"></asp:TextBox></td>
        
    </tr>
</table>