<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChipSelfInfor.ascx.cs" Inherits="ASCX_Chip_ChipSelfInfor" %>
<style type="text/css">
    .style1
    {
        height: 25px;
    }
</style>
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
<table cellspacing="1" cellpadding="0">
<tr>    
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Colum1Text" width="200px" height="22px" onkeydown="Colum1TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum1Text" ForeColor="Red" Display="Dynamic"  
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="Colum1Text" ForeColor="Red" Display="Dynamic" 
            ID="valeTxtItemName" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
        <asp:CompareValidator ID="CompareValidator2" runat="server" 
           ErrorMessage="请输入字符串" ControlToValidate="Colum1Text" Display="Dynamic" 
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
    </td>
    <td >
        <asp:DropDownList width="204px" height="28px" ID="Colum2Text" onkeydown="Colum2TextEnter()" runat="server">
        <asp:ListItem>0</asp:ListItem>
         <asp:ListItem>1</asp:ListItem>
        <asp:ListItem>2</asp:ListItem> 
        <asp:ListItem>3</asp:ListItem>  
        <asp:ListItem>4</asp:ListItem>     
        </asp:DropDownList>
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
       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
           ErrorMessage="不能为空" ControlToValidate="Colum3Text" ForeColor="Red" 
           SetFocusOnError="True" Display="Dynamic" ></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="Colum3Text" ForeColor="Red" Display="Dynamic"  
            ID="RegularExpressionValidator1" ValidationExpression="\S{1,500}" runat="server" ErrorMessage="字符长度1-500">
            </asp:RegularExpressionValidator>
       <asp:CompareValidator ID="CompareValidator1" runat="server" 
           ErrorMessage="请输入字符串" ControlToValidate="Colum3Text" Display="Dynamic" 
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>
<td width="150px" class="tdStyleBorderBK">
 <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
</td>
<td>
 <asp:DropDownList ID="Colum4Text" onkeydown="Colum4TextEnter()"  width="204px" height="28px" runat="server">
 <asp:ListItem>1</asp:ListItem>  
 <asp:ListItem>2</asp:ListItem>  
 <asp:ListItem>4</asp:ListItem> 
  </asp:DropDownList>
   <%-- <asp:Label ID="Label1" runat="server" Text="Bytes"></asp:Label> --%>
  </td>
</tr>   
   <tr>
   <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:DropDownList width="204px" height="28px" ID="Colum5Text" onkeydown="Colum5TextEnter()"  runat="server">
        <asp:ListItem>False</asp:ListItem> 
        <asp:ListItem>True</asp:ListItem> 
        </asp:DropDownList>
    </td>
   </tr>
</table>
</div>