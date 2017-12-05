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

                    var tempt = '<%=txtModelWeight.ClientID%>';
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

            function txtModelWeightEnter() {
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
<table cellspacing="1" cellpadding="0">
        <tr>
        <td width="150px" class="tdStyleBorderBK">
         别名</td>
        <td>
         <asp:TextBox width="200px" height="22px" ID="txtShowName" onkeydown="txtShowNameEnter()" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
            <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtShowName" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtShowName" ForeColor="Red" Display="Dynamic" 
            ID="valeTxtItemName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>
            
       </td>
       </tr>

       <tr>
        <td class="tdStyleBorderBK" width="150px">名称</td>
        <td>
        <asp:TextBox ID="txtItemName" width="200px" height="22px"  onkeydown="txtItemNameEnter()" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
            <asp:RequiredFieldValidator ID="valrTxtShowName" runat="server" 
                    ErrorMessage="不能为空" ControlToValidate="txtItemName" ForeColor="Red"  
                    SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" Display="Dynamic" 
                ID="valeTxtShowName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30 ">
                </asp:RegularExpressionValidator>          
        </td>
           
        </tr>

       <tr>
       <td class="tdStyleBorderBK" width="150px">
           应用类型
        </td>
        <td>
            <asp:DropDownList ID="ddlAppName"  width="204px" height="28px" onkeydown="ddlAppNameEnter()" runat="server">
            </asp:DropDownList>
        </td>
        </tr>
       <tr>
            <td class="tdStyleBorderBK1" width="150px">描述</td>
            <td>
             <asp:TextBox ID="txtItemDescription" width="198px" height="48px" onkeydown="txtItemDescriptionEnter()"  runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
            &nbsp;
            <asp:RegularExpressionValidator ControlToValidate="txtItemDescription" ForeColor="Red" Display="Dynamic" 
            ID="valeTxtItemDescription" ValidationExpression="\S{0,50}" runat="server" ErrorMessage="字符长度0-50">
            </asp:RegularExpressionValidator>
        </td>
        </tr>
       
        <tr>
        <td class="tdStyleBorderBK" width="150px">权重</td>
        <td>
        <asp:TextBox ID="txtModelWeight" width="200px" height="22px"  onkeydown="txtModelWeightEnter()" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
            <asp:RequiredFieldValidator ID="valrTxtModelWeight" runat="server" 
                    ErrorMessage="不能为空" ControlToValidate="txtModelWeight" ForeColor="Red"  
                    SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtModelWeight" ForeColor="Red" Display="Dynamic" 
                ID="RegularExpressionValidator1" ValidationExpression="\d\d?" runat="server" ErrorMessage="最大只能输入两位数">
                </asp:RegularExpressionValidator>          
        </td>
        </tr>

</table>