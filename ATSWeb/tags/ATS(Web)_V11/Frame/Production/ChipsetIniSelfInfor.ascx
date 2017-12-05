<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChipsetIniSelfInfor.ascx.cs" Inherits="ASCXChipsetIniSelfInfor" %>
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
       <asp:DropDownList ID="Colum2Text" width="205px" onkeydown="Colum5TextEnter()" runat="server">
        <asp:ListItem>LDD</asp:ListItem>
        <asp:ListItem>AMP</asp:ListItem>   
        <asp:ListItem>DAC </asp:ListItem>  
        <asp:ListItem>CDR</asp:ListItem>  
        </asp:DropDownList>
   </td>  
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Colum3Text" width="200px" Text="" onkeydown="Colum3TextEnter()" runat="server"></asp:TextBox>   
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum3Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator5" runat="server" 
            ErrorMessage="please input a number between 0-255" 
            ControlToValidate="Colum3Text" ForeColor="Red" MaximumValue="255" 
            MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    
   <td width="150px" class="tdStyleBorderBK">
   <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="Colum4Text" width="200px" onkeydown="Colum4TextEnter()" runat="server"></asp:TextBox>
   </td>
   <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
           ErrorMessage="not null" ControlToValidate="Colum4Text" ForeColor="Red" 
           SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator9" runat="server" 
            ErrorMessage="please input a int number " 
            ControlToValidate="Colum4Text" ForeColor="Red" MaximumValue="32767" 
            MinimumValue="-32768" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum5Text" width="200px" onkeydown="Colum5TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum5Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator10" runat="server" 
            ErrorMessage="please input a number between 0-255" 
            ControlToValidate="Colum5Text" ForeColor="Red" MaximumValue="255" 
            MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH6" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="Colum6Text" width="200px" onkeydown="Colum6TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum6Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator8" runat="server" 
            ErrorMessage="please input a int number " 
            ControlToValidate="Colum6Text" ForeColor="Red" MaximumValue="32767" 
            MinimumValue="-32768" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH7" runat="server" Text="LitEndian?"></asp:Label>
    </td>
    <td>     
        <asp:DropDownList ID="Colum7Text" onkeydown="Colum7TextEnter()" width="205px" runat="server">
        <asp:ListItem>False</asp:ListItem>
        <asp:ListItem>True</asp:ListItem>   
        </asp:DropDownList>
    </td>
   
</tr>

</table>
</div>