<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlowControlSelfInfor.ascx.cs" Inherits="ASCXFlowControlSelfInfor" %>
<script language="javascript" type="text/javascript">
    function Colum2TextEnter() {
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

            var tempt = '<%=Colum9Text.ClientID%>';
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
    function Colum9TextEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum10Text.ClientID%>';
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
    function Colum10TextEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum11Text.ClientID%>';
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
    function Colum11TextEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum12Text.ClientID%>';
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
    function Colum12TextEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=Colum13Text.ClientID%>';
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
    function Colum13TextEnter() {
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
<table cellspacing="1" cellpadding="0">
<tr>
   <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
   </td>
   <td>
    <asp:TextBox ID="Colum2Text" width="200px" height="22px" onkeydown="Colum2TextEnter()" runat="server" MaxLength="50"></asp:TextBox>
   </td>
   <td>
   &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
           ErrorMessage="不能为空" ControlToValidate="Colum2Text" ForeColor="Red" 
           SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="Colum2Text" ForeColor="Red" Display="Dynamic" 
            ID="RegularExpressionValidator3" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
       <asp:CompareValidator ID="CompareValidator1" runat="server" 
           ErrorMessage="请输入字符串" ControlToValidate="Colum2Text" Display="Dynamic"
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>    
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Colum4Text" width="200px" height="22px" onkeydown="Colum4TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum4Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" 
            ErrorMessage="请输入0-255之间的数字" 
            ControlToValidate="Colum4Text" ForeColor="Red" MaximumValue="255" 
            MinimumValue="0" SetFocusOnError="True" Type="Integer" Display="Dynamic"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Colum5Text" width="200px" height="22px" Text=""  onkeydown="Colum5TextEnter()" runat="server"></asp:TextBox>   
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum5Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator2" runat="server" 
            ErrorMessage="请输入单精度浮点数" ControlToValidate="Colum5Text" 
            ForeColor="Red" MaximumValue="1000000" MinimumValue="-1000000" 
            SetFocusOnError="True" Type="Double" Display="Dynamic"></asp:RangeValidator>
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
       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
           ErrorMessage="不能为空" ControlToValidate="Colum6Text" ForeColor="Red" 
           SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator3" runat="server" 
           ErrorMessage="请输入单精度浮点数" ControlToValidate="Colum6Text" 
           ForeColor="Red" MaximumValue="1000000" MinimumValue="-1000000" 
           SetFocusOnError="True" Type="Double" Display="Dynamic"></asp:RangeValidator>
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
       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum7Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator4" runat="server" 
            ErrorMessage="请输入0-255之间的数字" 
            ControlToValidate="Colum7Text" ForeColor="Red" MaximumValue="255" 
            MinimumValue="0" SetFocusOnError="True" Type="Integer" Display="Dynamic"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH8" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum8Text" width="200px" height="22px" onkeydown="Colum8TextEnter()" runat="server" MaxLength="50"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum8Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="Colum8Text" ForeColor="Red" Display="Dynamic" 
            ID="RegularExpressionValidator1" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
       <asp:CompareValidator ID="CompareValidator3" runat="server" 
            ErrorMessage="请输入字符串" ControlToValidate="Colum8Text" Display="Dynamic"
            ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH9" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:DropDownList ID="Colum9Text" width="204px" height="28px" onkeydown="Colum9TextEnter()" runat="server">
            </asp:DropDownList>
    </td>
    
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH10" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum10Text" width="200px" height="22px" onkeydown="Colum10TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum10Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator6" runat="server" 
            ErrorMessage="请输入单精度浮点数" 
            ControlToValidate="Colum10Text" ForeColor="Red" SetFocusOnError="True" Display="Dynamic"
            Type="Double" MaximumValue="1000000" MinimumValue="-1000000"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH11" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum11Text" width="200px" height="22px" onkeydown="Colum11TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum11Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator7" runat="server" 
            ErrorMessage="请输入单精度浮点数" 
            ControlToValidate="Colum11Text" ForeColor="Red" SetFocusOnError="True" Display="Dynamic"
            Type="Double" MaximumValue="1000000" MinimumValue="-1000000"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH12" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum12Text" width="200px" height="22px" onkeydown="Colum12TextEnter()" runat="server" MaxLength="200"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum12Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="Colum12Text" ForeColor="Red" Display="Dynamic"
            ID="RegularExpressionValidator2" ValidationExpression="\S{1,200}" runat="server" ErrorMessage="字符长度1-200">
            </asp:RegularExpressionValidator>
       <asp:CompareValidator ID="CompareValidator2" runat="server" 
            ErrorMessage="请输入字符串" ControlToValidate="Colum12Text" Display="Dynamic"
            ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH13" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="Colum13Text" width="204px" height="28px" onkeydown="Colum13TextEnter()" runat="server">
        <asp:ListItem>False</asp:ListItem>
        <asp:ListItem>True</asp:ListItem>        
        </asp:DropDownList>     
    </td>
</tr>
</table>
</div>