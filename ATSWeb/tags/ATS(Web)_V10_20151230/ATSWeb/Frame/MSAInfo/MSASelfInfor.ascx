<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MSASelfInfor.ascx.cs" Inherits="ASCXMSASelfInfor" %>
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
    <asp:TextBox ID="Colum2Text" width="200px" onkeydown="Colum2TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ErrorMessage="not null" ControlToValidate="Colum2Text" ForeColor="Red" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
            ControlToValidate="Colum2Text" runat="server" 
            ErrorMessage="RegularExpressionValidator" Display="Dynamic" 
            ForeColor="Red" ValidationExpression="(\w|\W){1,25}">the maximum namber is 25 characters</asp:RegularExpressionValidator>
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
       <asp:CompareValidator ID="CompareValidator2" runat="server" 
           ErrorMessage="please input a string" ControlToValidate="Colum3Text" 
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
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
</table>
</div>