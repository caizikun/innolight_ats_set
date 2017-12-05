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
            ID="valeTxtItemName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30 ">
            </asp:RegularExpressionValidator>
    </td>
    </tr>
    <tr>
    <td width="150px" class="tdStyleBorderBK">别名</td>
    <td>
    <asp:TextBox ID="txtShowName" width="200px" height="22px" onkeydown="txtShowNameEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
        <asp:RequiredFieldValidator ID="valrTxtShowName" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtShowName" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="txtShowName" ForeColor="Red" Display="Dynamic" 
            ID="valeTxtShowName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30 ">
            </asp:RegularExpressionValidator>
    </td>
    </tr>
    <tr>
    <td width="150px" class="tdStyleBorderBK">参数类型</td>
    <td>
    <asp:DropDownList ID="txtItemTypeDDList" width="204px" height="28px" onkeydown="txtItemTypeEnter()" AutoPostBack="true" runat="server" 
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
    <td width="150px" class="tdStyleBorderBK">值</td>
    <td>
    <asp:TextBox ID="txtItemValue" width="200px" height="22px" onkeydown="txtItemValueEnter()" 
            runat="server" MaxLength="255"></asp:TextBox>
    </td>
    <td>
    &nbsp;
        <asp:RequiredFieldValidator ID="valrTxtItemValue" runat="server" ErrorMessage="不能为空" ControlToValidate="txtItemValue" ForeColor="Red"  
             Display="Dynamic"></asp:RequiredFieldValidator>
        
        <asp:RangeValidator ID="valcTxtItemValue"  runat="server"  ControlToValidate="txtItemValue" ErrorMessage="请输入满足参数类型的值"
           ForeColor="Red" Display="Dynamic"  ></asp:RangeValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidatorItemValue" 
            runat="server" ControlToValidate="txtItemValue" 
            ErrorMessage="" ForeColor="Red"  Display="Dynamic"></asp:RegularExpressionValidator>
    </td>
    </tr>

    <tr>
    <td width="150px" class="tdStyleBorderBK">NeedSelect</td>
    <td>
        <asp:DropDownList ID="ddlNeedCheckParams"  width="204px" height="28px" onkeydown="ddlNeedCheckParamsEnter()" runat="server">
        <asp:ListItem>false</asp:ListItem>
        <asp:ListItem>true</asp:ListItem>        
        </asp:DropDownList>    
    </td>
    <td>
        
    </td>
    </tr>

    <tr>
    <td width="150px" class="tdStyleBorderBK">可选参数</td>
    <td>
    <asp:TextBox ID="txtOptionalparams" width="200px" height="22px" onkeydown="txtOptionalparamsEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
        <asp:RegularExpressionValidator ControlToValidate="txtOptionalparams" ForeColor="Red" Display="Dynamic" 
            ID="RegularExpressionValidator1" ValidationExpression="\S{0,4000}" runat="server" ErrorMessage="字符长度0-4000 ">
            </asp:RegularExpressionValidator>
        <%--<asp:RequiredFieldValidator ID="valrTxtOptionalparams" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtOptionalparams" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
         --%>        
    </td>
    </tr>
   <tr> 
        <td width="150px" class="tdStyleBorderBK1">描述</td> 
        <td><asp:TextBox ID="txtItemDescription" width="198px" height="48px" 
                onkeydown="txtItemDescriptionEnter()" runat="server" Rows="3" 
                TextMode="MultiLine" MaxLength="500"></asp:TextBox></td>
       <td>
       &nbsp;
        <asp:RegularExpressionValidator ControlToValidate="txtItemDescription" ForeColor="Red" Display="Dynamic" 
            ID="RegularExpressionValidator2" ValidationExpression="\S{0,500}" runat="server" ErrorMessage="字符长度0-500 ">
            </asp:RegularExpressionValidator>       
    </td>
    </tr>
</table>