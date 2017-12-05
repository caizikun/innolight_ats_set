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
<table style=" border:solid #000 1px">
<tr>
   <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
   </td>
   <td>
    <asp:TextBox ID="Colum2Text" Width="200px" onkeydown="Colum2TextEnter()" runat="server" MaxLength="50"></asp:TextBox>
   </td>
   <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
           ErrorMessage="not null" ControlToValidate="Colum2Text" ForeColor="Red" 
           SetFocusOnError="True"></asp:RequiredFieldValidator>
       <asp:CompareValidator ID="CompareValidator1" runat="server" 
           ErrorMessage="please input a string" ControlToValidate="Colum2Text" 
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>    
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Colum4Text" Width="200px" onkeydown="Colum4TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum4Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" 
            ErrorMessage="please input a number between 0-255" 
            ControlToValidate="Colum4Text" ForeColor="Red" MaximumValue="255" 
            MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Colum5Text" Width="200px" Text=""  onkeydown="Colum5TextEnter()" runat="server"></asp:TextBox>   
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum5Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator2" runat="server" 
            ErrorMessage="please input a  float number" ControlToValidate="Colum5Text" 
            ForeColor="Red" MaximumValue="1000000" MinimumValue="-1000000" 
            SetFocusOnError="True" Type="Double"></asp:RangeValidator>
   </td>
</tr>
<tr>
    
   <td width="150px" class="tdStyleBorderBK">
   <asp:Label ID="TH6" runat="server" Text="Label"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="Colum6Text" Width="200px" onkeydown="Colum6TextEnter()" runat="server"></asp:TextBox>
   </td>
   <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
           ErrorMessage="not null" ControlToValidate="Colum6Text" ForeColor="Red" 
           SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator3" runat="server" 
           ErrorMessage="please input a float number" ControlToValidate="Colum6Text" 
           ForeColor="Red" MaximumValue="1000000" MinimumValue="-1000000" 
           SetFocusOnError="True" Type="Double"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum7Text" Width="200px" onkeydown="Colum7TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum7Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator4" runat="server" 
            ErrorMessage="please input a number between 0-255" 
            ControlToValidate="Colum7Text" ForeColor="Red" MaximumValue="255" 
            MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH8" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum8Text" Width="200px" onkeydown="Colum8TextEnter()" runat="server" MaxLength="50"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum8Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
       <asp:CompareValidator ID="CompareValidator3" runat="server" 
            ErrorMessage="please input a string" ControlToValidate="Colum8Text" 
            ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH9" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:DropDownList ID="Colum9Text" Width="205px" onkeydown="Colum9TextEnter()" runat="server">
            </asp:DropDownList>
    </td>
    
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH10" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum10Text" Width="200px" onkeydown="Colum10TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum10Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator6" runat="server" 
            ErrorMessage="please input a number" 
            ControlToValidate="Colum10Text" ForeColor="Red" SetFocusOnError="True" 
            Type="Double" MaximumValue="1000000" MinimumValue="-1000000"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH11" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum11Text" Width="200px" onkeydown="Colum11TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum11Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator7" runat="server" 
            ErrorMessage="please input a number" 
            ControlToValidate="Colum11Text" ForeColor="Red" SetFocusOnError="True" 
            Type="Double" MaximumValue="1000000" MinimumValue="-1000000"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH12" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum12Text" Width="200px" onkeydown="Colum12TextEnter()" runat="server" MaxLength="200"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum12Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
       <asp:CompareValidator ID="CompareValidator2" runat="server" 
            ErrorMessage="please input a string" ControlToValidate="Colum12Text" 
            ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH13" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="Colum13Text" Width="205px" onkeydown="Colum13TextEnter()" runat="server">
        <asp:ListItem>False</asp:ListItem>
        <asp:ListItem>True</asp:ListItem>        
        </asp:DropDownList>     
    </td>
</tr>
</table>
</div>