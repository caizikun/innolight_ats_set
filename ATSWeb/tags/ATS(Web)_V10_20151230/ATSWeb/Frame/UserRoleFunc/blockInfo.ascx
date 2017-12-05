<%@ Control Language="C#" AutoEventWireup="true" CodeFile="blockInfo.ascx.cs" Inherits="Frame_UserRoleFunc_blockInfo" %>
<script language="javascript" type="text/javascript">
    function ddlParentItemEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {
            var tempt = '<%=ddlBlockLevel.ClientID%>';
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
    function ddlBlockLevelEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {
            var tempt = '<%=ddlBlockType.ClientID%>';
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
    function ddlBlockTypeEnter() {
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
            var tempt = '<%=txtAliasName.ClientID%>';
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
    function txtAliasNameEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {
            var tempt = '<%=txtTitle.ClientID%>';
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
    function txtTitleEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {
            var tempt = '<%=txtFunctionCode.ClientID%>';
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
    function txtFunctionCodeEnter() {
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
            var tempt = '<%=ddlParentItem.ClientID%>';
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
        <td width="150px" class="tdStyleBorderBK"><asp:Label ID="lblParentItem"   runat="server" Text="ParentItem" Visible="False"></asp:Label></td>
         <td>
            <asp:DropDownList ID="ddlParentItem" onkeydown="ddlParentItemEnter()" runat="server" Width="205px" Visible="False">
            <asp:ListItem> </asp:ListItem>
            
            </asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td width="150px" class="tdStyleBorderBK">BlockLevel</td> <td>
            <asp:DropDownList ID="ddlBlockLevel" onkeydown="ddlBlockLevelEnter()" runat="server" Width="205px" 
                AutoPostBack="True" onselectedindexchanged="ddlBlockLevel_SelectedIndexChanged">
            <asp:ListItem> </asp:ListItem>
            <asp:ListItem>0</asp:ListItem>
            
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150px" class="tdStyleBorderBK">BlockTypeID</td> <td>
            <asp:DropDownList ID="ddlBlockType" onkeydown="ddlBlockTypeEnter()" runat="server" Width="205px" 
                AutoPostBack="True" onselectedindexchanged="ddlBlockType_SelectedIndexChanged">
            <asp:ListItem>NewBlock</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150px" class="tdStyleBorderBK">ItemName</td> <td>
            <asp:TextBox ID="txtItemName" onkeydown="txtItemNameEnter()" Width="200px" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtItemName" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" 
            ID="valeTxtItemName" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
            
       </td>
    </tr>
    <tr>
        <td width="150px" class="tdStyleBorderBK">AliasName</td> <td>
            <asp:TextBox ID="txtAliasName"  onkeydown="txtAliasNameEnter()" Width="200px" runat="server"></asp:TextBox>
        </td>
        <td>
        <asp:RegularExpressionValidator ControlToValidate="txtAliasName" ForeColor="Red" 
            ID="valcTxtAliasName" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td width="150px" class="tdStyleBorderBK"><asp:Label ID="lblTitle" runat="server" Text="Title" Visible="False"></asp:Label></td>
         <td>
            <asp:TextBox ID="txtTitle" onkeydown="txtTitleEnter()" Width="200px" runat="server" Visible="False"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtTitle" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtTitle" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtTitle" ForeColor="Red" 
            ID="valeTxtTitle" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
            
       </td>
    </tr>
    <tr>      

        <td width="150px" class="tdStyleBorderBK"><asp:Label ID="lblFunctionCode" runat="server" Text="FunctionCode" Visible="False"></asp:Label></td> <td>
            <asp:TextBox ID="txtFunctionCode"  onkeydown="txtFunctionCodeEnter()" Width="200px" runat="server" Visible="False"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtFunctionCode" runat="server" 
            ErrorMessage="not null" ControlToValidate="txtFunctionCode" ForeColor="Red"  
            SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="valcTxtFunctionCode" runat="server" 
            ErrorMessage="please input a double value" ControlToValidate="txtFunctionCode" Type="Integer"
            ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
            
        </td>
    </tr>
    <tr>
        <td width="150px" class="tdStyleBorderBK"><asp:Label ID="lblRemarks" runat="server" Text="Remarks" Visible="False"></asp:Label></td> <td>
            <asp:TextBox ID="txtRemarks" onkeydown="txtRemarksEnter()" Width="200px" runat="server" Visible="False"></asp:TextBox>
        </td>
         <td>
         <asp:RegularExpressionValidator ControlToValidate="txtRemarks" ForeColor="Red" 
            ID="valeTxtRemarks" ValidationExpression="\S{0,100}" runat="server" ErrorMessage="字符长度0-100">
            </asp:RegularExpressionValidator>
        </td>
    </tr>
    
</table>