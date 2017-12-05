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
<table cellspacing="1" cellpadding="0" >
    <tr>
        <td id="ParentItemTd" width="150px" class="tdStyleBorderBK" style="display:none;" runat="server"><asp:Label ID="lblParentItem"   runat="server" Text="上一级名称" Visible="False" style=" line-height :27px;" ></asp:Label></td>
         <td>
            <asp:DropDownList ID="ddlParentItem" onkeydown="ddlParentItemEnter()" runat="server" width="204px" height="28px" Visible="False">
            <asp:ListItem> </asp:ListItem>
            
            </asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td width="150px" class="tdStyleBorderBK">功能块级数</td> <td>
            <asp:DropDownList ID="ddlBlockLevel" onkeydown="ddlBlockLevelEnter()" runat="server" width="204px" height="28px" 
                AutoPostBack="True" onselectedindexchanged="ddlBlockLevel_SelectedIndexChanged">                       
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150px" class="tdStyleBorderBK">所属功能块</td> <td>
            <asp:DropDownList ID="ddlBlockType" onkeydown="ddlBlockTypeEnter()" runat="server" width="204px" height="28px" 
                AutoPostBack="True" onselectedindexchanged="ddlBlockType_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150px" class="tdStyleBorderBK">名称</td> <td>
            <asp:TextBox ID="txtItemName" onkeydown="txtItemNameEnter()" width="200px" height="22px" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
            <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtItemName" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" Display="Dynamic"
            ID="valeTxtItemName" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
            
       </td>
    </tr>
    <tr>
        <td width="150px" class="tdStyleBorderBK">别名</td> <td>
            <asp:TextBox ID="txtAliasName"  onkeydown="txtAliasNameEnter()" width="200px" height="22px" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
        <asp:RegularExpressionValidator ControlToValidate="txtAliasName" ForeColor="Red" Display="Dynamic"
            ID="valcTxtAliasName" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td id="TitleTd" width="150px" class="tdStyleBorderBK" style="display:block;" runat="server"><asp:Label ID="lblTitle" runat="server" Text="标题" style=" line-height :27px;"></asp:Label></td>
         <td>
            <asp:TextBox ID="txtTitle" onkeydown="txtTitleEnter()" width="200px" height="22px" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
<%--            <asp:RequiredFieldValidator ID="valrTxtTitle" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtTitle" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
            <asp:RegularExpressionValidator ControlToValidate="txtTitle" ForeColor="Red" Display="Dynamic"
            ID="valeTxtTitle" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
            
       </td>
    </tr>
    <tr>      

        <td id="FunctionCodeTd" width="150px" class="tdStyleBorderBK" style="display:block;" runat="server"><asp:Label ID="lblFunctionCode" runat="server" Text="权限码" style=" line-height :27px;" ></asp:Label></td> <td>
            <asp:TextBox ID="txtFunctionCode"  onkeydown="txtFunctionCodeEnter()" width="200px" height="22px" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
            <asp:RequiredFieldValidator ID="valrTxtFunctionCode" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="txtFunctionCode" ForeColor="Red"  
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="valcTxtFunctionCode" runat="server" 
            ErrorMessage="请输入整数" ControlToValidate="txtFunctionCode" Type="Integer" Display="Dynamic"
            ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
            
        </td>
    </tr>
    <tr>
        <td id="RemarksTd" width="150px" class="tdStyleBorderBK" style="display:block;" runat="server"><asp:Label ID="lblRemarks" runat="server" Text="备注" style=" line-height :27px;"></asp:Label></td> <td>
            <asp:TextBox ID="txtRemarks" onkeydown="txtRemarksEnter()" width="200px" height="22px" runat="server" ></asp:TextBox>
        </td>
         <td>
         &nbsp;
         <asp:RegularExpressionValidator ControlToValidate="txtRemarks" ForeColor="Red" Display="Dynamic"
            ID="valeTxtRemarks" ValidationExpression="\S{0,100}" runat="server" ErrorMessage="字符长度0-100">
            </asp:RegularExpressionValidator>
        </td>
    </tr>
    
</table>