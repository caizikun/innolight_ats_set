<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FormulaINFor.ascx.cs" Inherits="ASCX_Chip_FormulaINFor" %>
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
    <asp:Label ID="TH1" runat="server" Text="Name"></asp:Label>
    </td>
    <td>
   <asp:TextBox ID="Colum1Text" onkeydown="Colum1TextEnter()"  width="200px" runat="server"></asp:TextBox>

  </td>
     <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum1Text" ForeColor="Red" 
            SetFocusOnError="True" Enabled="False"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator2" runat="server" 
           ErrorMessage="please input a string" ControlToValidate="Colum1Text" 
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
  
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH2" runat="server" Text="WriteFormula"></asp:Label>
    </td>
    <td >
       <asp:TextBox ID="Colum2Text" width="200px" onkeydown="Colum2TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum2Text" ForeColor="Red" 
            SetFocusOnError="True" Enabled="False"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator3" runat="server" 
           ErrorMessage="please input a string" ControlToValidate="Colum2Text" 
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>
<td width="150px" class="tdStyleBorderBK">
 <asp:Label ID="TH4" runat="server" Text="AnalogueUnit"></asp:Label>
</td>
<td>
 <asp:DropDownList ID="Colum3Text" onkeydown="Colum3TextEnter()"  width="205px" runat="server">
 <asp:ListItem>mA</asp:ListItem>  
 <asp:ListItem>mV</asp:ListItem>  
  <asp:ListItem> </asp:ListItem>  
  </asp:DropDownList></td>
</tr>
<tr>
    
   <td width="150px" class="tdStyleBorderBK">
   <asp:Label ID="TH3" runat="server" Text="ReadFormula"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="Colum4Text" width="200px" onkeydown="Colum4TextEnter()" runat="server"></asp:TextBox>
   </td>
   <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
           ErrorMessage="not null" ControlToValidate="Colum4Text" ForeColor="Red" 
           SetFocusOnError="True" Enabled="False"></asp:RequiredFieldValidator>
       <asp:CompareValidator ID="CompareValidator1" runat="server" 
           ErrorMessage="please input a string" ControlToValidate="Colum4Text" 
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH5" runat="server" Text="Address(Dec)"></asp:Label>
    </td>
    <td >
       <asp:TextBox ID="Colum5Text" width="200px" onkeydown="Colum5TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum5Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" 
            ErrorMessage="please input number 0-32767" ForeColor="Red" MaximumValue="32767" 
            MinimumValue="0" Type="Integer" ControlToValidate="Colum5Text"></asp:RangeValidator>
   </td>
    
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH6" runat="server" Text="StartBit"></asp:Label>
    </td>
    <td >
       <asp:TextBox ID="Colum6Text" width="200px" onkeydown="Colum6TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum6Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator2" runat="server" 
            ErrorMessage="please input number 0-255" ForeColor="Red" MaximumValue="255" 
            MinimumValue="0" Type="Integer" ControlToValidate="Colum6Text"></asp:RangeValidator>
   </td>
</tr>
<tr>    
    <td width="150px" class="tdStyleBorderBK">
        EndBit</td>
    <td>    
    
         <asp:TextBox runat="server" ID="Colum7Text" onkeydown="Colum7TextEnter()"  width="200px" >
         </asp:TextBox>
</td>
     <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum7Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator4" runat="server" 
            ErrorMessage="please input number 0-255" ForeColor="Red" MaximumValue="255" 
            MinimumValue="0" Type="Integer" ControlToValidate="Colum7Text"></asp:RangeValidator>
   </td>
  
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="Label3" runat="server" Text="UnitLength"></asp:Label>
    </td>   
    <td>
 <asp:DropDownList ID="Colum8Text" onkeydown="Colum8TextEnter()"  width="205px" runat="server">
 <asp:ListItem>1</asp:ListItem>  
 <asp:ListItem>2</asp:ListItem>  
 <asp:ListItem>4</asp:ListItem> 
  </asp:DropDownList></td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum8Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator3" runat="server" 
            ErrorMessage="please input number 0-32" ForeColor="Red" MaximumValue="32" 
            MinimumValue="0" Type="Integer" ControlToValidate="Colum8Text"></asp:RangeValidator>
   </td>
</tr>
<tr>
<td width="150px" class="tdStyleBorderBK">
 <asp:Label ID="TH9" runat="server" Text="ChipLine"></asp:Label>
</td>
<td>
 <asp:DropDownList ID="Colum9Text" onkeydown="Colum9TextEnter()"  width="205px" runat="server"> 
  </asp:DropDownList></td>
</tr>
 
</table>
</div>