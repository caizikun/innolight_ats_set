<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PNSpecInfor.ascx.cs" Inherits="ASCXPNSpecInfor" %>
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
<table cellspacing="1" cellpadding="0">
<tr>    
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
    <asp:DropDownList ID="Colum2Text" width="204px" height="28px" onkeydown="Colum2TextEnter()" AutoPostBack="true" runat="server" 
            onselectedindexchanged="Colum2Text_SelectedIndexChanged">
            </asp:DropDownList>
   </td>
</tr>
<tr>
<td width="150px" class="tdStyleBorderBK">
 <asp:Label ID="TH6" runat="server" Text="单位"></asp:Label>
</td>
<td>
 <asp:Label ID="Colum6Text" runat="server" Text="Label"></asp:Label>
</td>
</tr>
<tr>
   <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>
   </td>
   <td>
    <asp:TextBox ID="Colum3Text" width="200px" height="22px" AutoPostBack="false" onkeydown="Colum3TextEnter()"  runat="server" 
         ></asp:TextBox>
   </td>
   <td>
   &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
           ErrorMessage="不能为空" ControlToValidate="Colum3Text" ForeColor="Red" 
           SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
   </td>
    
   <td><asp:RangeValidator ID="RangeValidatorTypical" runat="server" 
           ErrorMessage="请确认输入值在最大值和最小值之间" ForeColor="Red" 
           MaximumValue="1000000" MinimumValue="-1000000" Type="Double" 
           ControlToValidate="Colum3Text" Display="Dynamic"></asp:RangeValidator></td>
</tr>
<tr>
   <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>
   </td>
   <td>
    <asp:TextBox ID="Colum4Text" width="200px" height="22px" AutoPostBack="false" onkeydown="Colum4TextEnter()"  runat="server" 
           ></asp:TextBox>
   </td>
   <td>
   &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
           ErrorMessage="不能为空" ControlToValidate="Colum4Text" ForeColor="Red" 
           SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
   </td>
    
   <td><asp:RangeValidator ID="RangeValidatorMin" runat="server" 
           ErrorMessage="请确认须小于最大值" ForeColor="Red" 
           MaximumValue="1000000" MinimumValue="-1000000" Type="Double" 
           ControlToValidate="Colum4Text" Display="Dynamic"></asp:RangeValidator></td>
</tr>
<tr>
   <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>
   </td>
   <td>
    <asp:TextBox ID="Colum5Text" width="200px" height="22px" AutoPostBack="false" onkeydown="Colum5TextEnter()"  runat="server" 
           ></asp:TextBox>
   </td>
   <td>
   &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
           ErrorMessage="不能为空" ControlToValidate="Colum5Text" ForeColor="Red" 
           SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
   </td>
    
   <td><asp:RangeValidator ID="RangeValidatorMax" runat="server" 
           ErrorMessage="请确认大于最小值" ForeColor="Red" 
           MaximumValue="1000000" MinimumValue="-1000000" Type="Double" 
           ControlToValidate="Colum5Text" Display="Dynamic"></asp:RangeValidator></td>
</tr>
<tr>    
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
    <asp:DropDownList ID="Colum7Text" width="204px" height="28px" onkeydown="Colum7TextEnter()" AutoPostBack="true" runat="server" 
            >
            </asp:DropDownList>
   </td>
</tr>
</table>
</div>