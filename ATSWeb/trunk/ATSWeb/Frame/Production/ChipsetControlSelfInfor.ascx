<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChipsetControlSelfInfor.ascx.cs" Inherits="ASCXChipsetControlSelfInfor" %>
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
<table cellspacing="1" cellpadding="0">
<tr>
   <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
   </td>
   <td style="position:relative;">
    <asp:DropDownList ID="ddlItem" runat="server" width="204px" height="28px" AutoPostBack="True" 
        onselectedindexchanged="ddlItem_SelectedIndexChanged" onkeydown="ddlItemEnter()">
    </asp:DropDownList>
    <asp:TextBox ID="Colum2Text" width="179px" height="22px" onkeydown="Colum2TextEnter()" 
           runat="server" 
           style="position:absolute; margin-left:-208px;"></asp:TextBox>
   </td>
   <td>
   &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
           ErrorMessage="不能为空" ControlToValidate="Colum2Text" ForeColor="Red" 
           SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="Colum2Text" ForeColor="Red" Display="Dynamic"
            ID="RegularExpressionValidator1" ValidationExpression="\S{1,20}" runat="server" ErrorMessage="字符长度1-20">
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
        <asp:RangeValidator ID="RangeValidator5" runat="server" 
            ErrorMessage="请输入0-255之间的数字" 
            ControlToValidate="Colum4Text" ForeColor="Red" MaximumValue="255" Display="Dynamic"
            MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    
   <td width="150px" class="tdStyleBorderBK">
   <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
   </td>
   <td>       
       <asp:DropDownList ID="Colum5Text" width="204px" height="28px" onkeydown="Colum5TextEnter()" runat="server">
        <asp:ListItem>LDD</asp:ListItem>
        <asp:ListItem>AMP</asp:ListItem>   
        <asp:ListItem>DAC </asp:ListItem>  
        <asp:ListItem>CDR</asp:ListItem>  
        </asp:DropDownList>
   </td>
   <td>
       &nbsp;</td>
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
     <asp:TextBox ID="Colum7Text" width="200px" height="22px"  onkeydown="Colum7TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum7Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator8" runat="server" 
            ErrorMessage="请输入整数" 
            ControlToValidate="Colum7Text" ForeColor="Red" MaximumValue="32767" Display="Dynamic"
            MinimumValue="-32768" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH9" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum9Text" width="200px" height="22px"  onkeydown="Colum9TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum9Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" 
            ErrorMessage="请输入字节长度范围内的整数" 
            ControlToValidate="Colum9Text" ForeColor="Red" Display="Dynamic"
            SetFocusOnError="True" Type="Integer" MaximumValue="255" MinimumValue="0"></asp:RangeValidator>       
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH10" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum10Text" width="200px" height="22px"  onkeydown="Colum10TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum10Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator2" runat="server" 
            ErrorMessage="请输入字节长度范围内的整数" 
            ControlToValidate="Colum10Text" ForeColor="Red" Display="Dynamic"
            SetFocusOnError="True" Type="Integer" MaximumValue="255" MinimumValue="0"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH8" runat="server" Text="低位在前高位在后?"></asp:Label>
    </td>
    <td>     
        <asp:DropDownList ID="Colum8Text" width="204px" height="28px" onkeydown="Colum8TextEnter()" runat="server">
        <asp:ListItem>False</asp:ListItem>
        <asp:ListItem>True</asp:ListItem>   
        </asp:DropDownList>
    </td>   
</tr>
</table>
</div>