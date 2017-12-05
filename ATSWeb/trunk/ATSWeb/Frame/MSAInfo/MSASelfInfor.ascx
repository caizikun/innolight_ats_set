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
<table cellspacing="1" cellpadding="0">
<tr>    
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Colum2Text" width="200px" height="22px" onkeydown="Colum2TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum2Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
            ControlToValidate="Colum2Text" runat="server" 
            ErrorMessage="RegularExpressionValidator" Display="Dynamic" 
            ForeColor="Red" ValidationExpression="(\w|\W){1,25}">字符长度1-25</asp:RegularExpressionValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Colum3Text" width="200px" height="22px" Text="" onkeydown="Colum3TextEnter()" runat="server"></asp:TextBox>   
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum3Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="Colum3Text" ForeColor="Red" Display="Dynamic" 
            ID="RegularExpressionValidator2" ValidationExpression="\S{1,25}" runat="server" ErrorMessage="字符长度1-25">
            </asp:RegularExpressionValidator>
       <asp:CompareValidator ID="CompareValidator2" runat="server" 
           ErrorMessage="请输入字符串" ControlToValidate="Colum3Text" Display="Dynamic"
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
       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
           ErrorMessage="不能为空" ControlToValidate="Colum4Text" ForeColor="Red" 
           SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator9" runat="server" 
            ErrorMessage="请输入整数" 
            ControlToValidate="Colum4Text" ForeColor="Red" MaximumValue="32767" Display="Dynamic"
            MinimumValue="-32768" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
   </td>
</tr>
</table>
</div>