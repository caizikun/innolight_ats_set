<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegisterINfor.ascx.cs" Inherits="Frame_Chip_RegisterINfor" %>
<script language="javascript" type="text/javascript">
    function Colum1TextEnter() {
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

            var tempt = '<%=Colum1Text.ClientID%>';
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
    <asp:Label ID="TH2" runat="server" Text="Address(Dec)"></asp:Label>
    </td>
    <td >
       <asp:TextBox ID="Colum2Text" width="200px" onkeydown="Colum2TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum2Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" 
            ErrorMessage="please input number 0-32767" ForeColor="Red" MaximumValue="32767" 
            MinimumValue="0" Type="Integer" ControlToValidate="Colum2Text"></asp:RangeValidator>
   </td>
    
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="Label1" runat="server" Text="StartBit"></asp:Label>
    </td>
    <td >
       <asp:TextBox ID="Colum3Text" width="200px" onkeydown="Colum3TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum3Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator2" runat="server" 
            ErrorMessage="please input number 0-255" ForeColor="Red" MaximumValue="255" 
            MinimumValue="0" Type="Integer" ControlToValidate="Colum3Text"></asp:RangeValidator>
   </td>
</tr>
<tr>    
    <td width="150px" class="tdStyleBorderBK">
        EndBit</td>
    <td>    
    
         <asp:TextBox runat="server" ID="Colum1Text" onkeydown="Colum1TextEnter()"  width="200px" >
         </asp:TextBox>
</td>
     <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum1Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator4" runat="server" 
            ErrorMessage="please input number 0-255" ForeColor="Red" MaximumValue="255" 
            MinimumValue="0" Type="Integer" ControlToValidate="Colum1Text"></asp:RangeValidator>
   </td>
  
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="Label2" runat="server" Text="UnitLength"></asp:Label>
    </td>   
    <td>
 <asp:DropDownList ID="Colum4Text" onkeydown="Colum4TextEnter()"  width="205px" runat="server">
 <asp:ListItem>1</asp:ListItem>  
 <asp:ListItem>2</asp:ListItem>  
 <asp:ListItem>4</asp:ListItem> 
  </asp:DropDownList></td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum4Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator3" runat="server" 
            ErrorMessage="please input number 0-32" ForeColor="Red" MaximumValue="32" 
            MinimumValue="0" Type="Integer" ControlToValidate="Colum4Text"></asp:RangeValidator>
   </td>
</tr>
<tr>
<td width="150px" class="tdStyleBorderBK">
 <asp:Label ID="TH5" runat="server" Text="ChipLine"></asp:Label>
</td>
<td>
 <asp:DropDownList ID="Colum5Text" onkeydown="Colum5TextEnter()"  width="205px" runat="server"> 
  </asp:DropDownList></td>
</tr>
</table>
</div>