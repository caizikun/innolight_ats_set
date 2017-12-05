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
<table cellspacing="1" cellpadding="0">
<tr>    
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
    </td>
    <td>
   <asp:TextBox ID="Colum1Text" onkeydown="Colum1TextEnter()"  width="200px" height="22px" runat="server"></asp:TextBox>

  </td>
     <td>
     &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum1Text" ForeColor="Red" Display="Dynamic" 
            SetFocusOnError="True" Enabled="False"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="Colum1Text" ForeColor="Red" Display="Dynamic" 
            ID="RegularExpressionValidator1" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
        <asp:CompareValidator ID="CompareValidator2" runat="server" 
           ErrorMessage="请输入字符串" ControlToValidate="Colum1Text" Display="Dynamic"
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
  
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH2" runat="server" Text="写公式"></asp:Label>
    </td>
    <td >
       <asp:TextBox ID="Colum2Text" width="200px" height="22px" onkeydown="Colum2TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum2Text" ForeColor="Red" Display="Dynamic"
            SetFocusOnError="True" Enabled="False"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="Colum2Text" ForeColor="Red" Display="Dynamic" 
            ID="RegularExpressionValidator2" ValidationExpression="\S{1,200}" runat="server" ErrorMessage="字符长度1-200">
            </asp:RegularExpressionValidator>
        <asp:CompareValidator ID="CompareValidator3" runat="server" 
           ErrorMessage="请输入字符串" ControlToValidate="Colum2Text" Display="Dynamic"
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>
<td width="150px" class="tdStyleBorderBK">
 <asp:Label ID="TH4" runat="server" Text="模拟量单位"></asp:Label>
</td>
<td>
 <asp:DropDownList ID="Colum3Text" onkeydown="Colum3TextEnter()"  width="204px" height="28px" runat="server">
 <asp:ListItem>mA</asp:ListItem>  
 <asp:ListItem>mV</asp:ListItem>  
  <asp:ListItem> </asp:ListItem>  
  </asp:DropDownList></td>
</tr>
<tr>
    
   <td width="150px" class="tdStyleBorderBK">
   <asp:Label ID="TH3" runat="server" Text="读公式"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="Colum4Text" width="200px" height="22px" onkeydown="Colum4TextEnter()" runat="server"></asp:TextBox>
   </td>
   <td>
   &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
           ErrorMessage="不能为空" ControlToValidate="Colum4Text" ForeColor="Red" Display="Dynamic" 
           SetFocusOnError="True" Enabled="False"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="Colum4Text" ForeColor="Red" Display="Dynamic" 
            ID="RegularExpressionValidator3" ValidationExpression="\S{1,200}" runat="server" ErrorMessage="字符长度1-200">
            </asp:RegularExpressionValidator>
       <asp:CompareValidator ID="CompareValidator1" runat="server" 
           ErrorMessage="请输入字符串" ControlToValidate="Colum4Text" Display="Dynamic"
           ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
   </td>
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH5" runat="server" Text="地址(Dec)"></asp:Label>
    </td>
    <td >
       <asp:TextBox ID="Colum5Text" width="200px" height="22px" onkeydown="Colum5TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum5Text" ForeColor="Red" Display="Dynamic"
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" 
            ErrorMessage="请输入0-32767之间的数字" ForeColor="Red" MaximumValue="32767" Display="Dynamic"
            MinimumValue="0" Type="Integer" ControlToValidate="Colum5Text"></asp:RangeValidator>
   </td>
    
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="TH6" runat="server" Text="开始位"></asp:Label>
    </td>
    <td >
       <asp:TextBox ID="Colum6Text" width="200px" height="22px" onkeydown="Colum6TextEnter()" runat="server"></asp:TextBox>
    </td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum6Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator2" runat="server" 
            ErrorMessage="请输入0-255之间的数字" ForeColor="Red" MaximumValue="255"  Display="Dynamic"
            MinimumValue="0" Type="Integer" ControlToValidate="Colum6Text"></asp:RangeValidator>
   </td>
</tr>
<tr>    
    <td width="150px" class="tdStyleBorderBK">
        结束位</td>
    <td>    
    
         <asp:TextBox runat="server" ID="Colum7Text" onkeydown="Colum7TextEnter()"  width="200px" height="22px" >
         </asp:TextBox>
</td>
     <td>
     &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum7Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator4" runat="server" 
            ErrorMessage="请输入0-255之间的数字" ForeColor="Red" MaximumValue="255" Display="Dynamic"
            MinimumValue="0" Type="Integer" ControlToValidate="Colum7Text"></asp:RangeValidator>
   </td>
  
</tr>
<tr>
    <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="Label3" runat="server" Text="单位长度"></asp:Label>
    </td>   
    <td>
 <asp:DropDownList ID="Colum8Text" onkeydown="Colum8TextEnter()"  width="204px" height="28px" runat="server">
 <asp:ListItem>1</asp:ListItem>  
 <asp:ListItem>2</asp:ListItem>  
 <asp:ListItem>4</asp:ListItem> 
  </asp:DropDownList></td>
    <td>
    &nbsp;
       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="Colum8Text" ForeColor="Red" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator3" runat="server" 
            ErrorMessage="请输入0-32之间的数字" ForeColor="Red" MaximumValue="32" Display="Dynamic"
            MinimumValue="0" Type="Integer" ControlToValidate="Colum8Text"></asp:RangeValidator>
   </td>
</tr>
<tr>
<td width="150px" class="tdStyleBorderBK">
 <asp:Label ID="TH9" runat="server" Text="芯片通道"></asp:Label>
</td>
<td>
 <asp:DropDownList ID="Colum9Text" onkeydown="Colum9TextEnter()"  width="204px" height="28px" runat="server"> 
  </asp:DropDownList></td>
</tr>
 
</table>
</div>