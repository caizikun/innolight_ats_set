<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModuleTypeSelfInfor.ascx.cs" Inherits="ASCXModuleTypeSelfInfor" %>
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
    <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
    <asp:DropDownList ID="Colum3Text" width="205px" onkeydown="Colum3TextEnter()" runat="server">
            </asp:DropDownList>
   </td>
</tr>
</table>
</div>