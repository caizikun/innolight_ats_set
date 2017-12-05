<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GloblaMSADefinationSelfInfor.ascx.cs" Inherits="ASCXGloblaMSADefinationSelfInfor" %>
<script language="javascript" type="text/javascript">
    function Colum2TextEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum3Text.ClientID%>';
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
    function ddlItemEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum3Text.ClientID%>';
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
    function Colum3TextEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum4Text.ClientID%>';
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
    function Colum4TextEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum5Text.ClientID%>';
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
    function Colum5TextEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum6Text.ClientID%>';
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
    function Colum6TextEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum7Text.ClientID%>';
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
    function Colum7TextEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum8Text.ClientID%>';
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
    function Colum8TextEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum2Text.ClientID%>';
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
<div>
<table  cellspacing="1" cellpadding="0">
<tr>
   <td  width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
   </td>
   <td style="position:relative;">
    <asp:DropDownList ID="ddlItem" runat="server" width="204px" height="28px" AutoPostBack="True" 
        onselectedindexchanged="ddlItem_SelectedIndexChanged" onkeydown="ddlItemEnter()">
    </asp:DropDownList>
    <asp:TextBox ID="Colum2Text" width="179px" height="22px" onkeydown="Colum2TextEnter()" runat="server" style="position:absolute; margin-left:-208px;"></asp:TextBox>
   </td>
   <td>
   &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
           ErrorMessage="不能为空" ControlToValidate="Colum2Text" ForeColor="Red" 
           SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="Colum2Text" ForeColor="Red" Display="Dynamic"
            ID="RegularExpressionValidator1" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>
       <asp:CompareValidator ID="CompareValidator1" runat="server" 
           ErrorMessage="请输入字符串" ControlToValidate="Colum2Text" Display="Dynamic" 
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>    
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Colum3Text" width="200px" height="22px" onkeydown="Colum3TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum3Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator7" runat="server" 
            ErrorMessage="请输入0-255之间的数字" 
            ControlToValidate="Colum3Text" ForeColor="Red" MaximumValue="255" Display="Dynamic"
            MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Colum4Text" width="200px" height="22px" Text="" onkeydown="Colum4TextEnter()" runat="server"></asp:TextBox>   
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum4Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator8" runat="server" 
            ErrorMessage="请输入整数" 
            ControlToValidate="Colum4Text" ForeColor="Red" MaximumValue="32767" Display="Dynamic"
            MinimumValue="-32768" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    
   <td width="150px" class="tdStyleBorderBK">
   <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="Colum5Text" width="200px" height="22px" onkeydown="Colum5TextEnter()" runat="server"></asp:TextBox>
   </td>
   <td>
   &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
           ErrorMessage="不能为空" ControlToValidate="Colum5Text" ForeColor="Red" 
           SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator6" runat="server" 
            ErrorMessage="请输入0-255之间的数字" 
            ControlToValidate="Colum5Text" ForeColor="Red" MaximumValue="255" Display="Dynamic"
            MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH6" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum6Text" width="200px" height="22px" onkeydown="Colum6TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum6Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator4" runat="server" 
            ErrorMessage="请输入整数" 
            ControlToValidate="Colum6Text" ForeColor="Red" MaximumValue="32767" Display="Dynamic"
            MinimumValue="-32768" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum7Text" width="200px" height="22px" onkeydown="Colum7TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum7Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator9" runat="server" 
            ErrorMessage="请输入0-255之间的数字" 
            ControlToValidate="Colum7Text" ForeColor="Red" MaximumValue="255" Display="Dynamic"
            MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH8" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum8Text" width="200px" height="22px" onkeydown="Colum8TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum8Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="Colum8Text" ForeColor="Red" Display="Dynamic"
            ID="RegularExpressionValidator2" ValidationExpression="\S{1,10}" runat="server" ErrorMessage="字符长度1-10">
            </asp:RegularExpressionValidator>
       <asp:CompareValidator ID="CompareValidator2" runat="server" 
            ErrorMessage="请输入字符串" ControlToValidate="Colum8Text" Display="Dynamic"
            ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>

</table>
</div>