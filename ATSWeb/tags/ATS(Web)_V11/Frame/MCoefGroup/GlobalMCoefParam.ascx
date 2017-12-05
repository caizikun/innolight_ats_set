<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalMCoefParam.ascx.cs" Inherits="Frame_MCoefGroup_GlobalMCoefParam" %>
<script language="javascript" type="text/javascript">
    function ddlItemTypeEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=txtItemName.ClientID%>';
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
    function txtItemNameEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=ddlChannel.ClientID%>';
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
    function ddlItemEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=ddlChannel.ClientID%>';
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
    function ddlChannelEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=txtPage.ClientID%>';
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
    function txtPageEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=txtStartAddress.ClientID%>';
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
    function txtStartAddressEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=txtLength.ClientID%>';
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
    function txtLengthEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=ddlFormat.ClientID%>';
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
    function ddlFormatEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=ddlItemType.ClientID%>';
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
<br />
<table style=" border:solid #000 1px">

    <tr> 
        <td width="150px" class="tdStyleBorderBK">Type</td> 
        <td><asp:DropDownList ID="ddlItemType" width="155px"  
                onkeydown="ddlItemTypeEnter()" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlItemType_SelectedIndexChanged">
            <asp:ListItem>Firmware</asp:ListItem>
            <asp:ListItem>ADC</asp:ListItem>
            <asp:ListItem>Coefficient</asp:ListItem>
            <asp:ListItem>APC config</asp:ListItem>
            <asp:ListItem>threshold</asp:ListItem>
            </asp:DropDownList>
         </td>
    </tr>

    <tr> 
        <td width="150px" class="tdStyleBorderBK">Item</td> 
        <td style="position:relative;">
        <asp:DropDownList ID="ddlItem" runat="server" width="155px" AutoPostBack="True" 
                onselectedindexchanged="ddlItem_SelectedIndexChanged" onkeydown="ddlItemEnter()">
            </asp:DropDownList>
        <asp:TextBox ID="txtItemName" width="132px" height="13px" onkeydown="txtItemNameEnter()" runat="server" style="position:absolute; margin-left:-159px;"></asp:TextBox>        
        </td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtItemName" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" 
            ID="valeTxtItemName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>           
       </td>
    </tr>
    
    <tr> 
        <td width="150px"  class="tdStyleBorderBK">Channel</td> 
        <td>
            <asp:DropDownList ID="ddlChannel" width="155px"  onkeydown="ddlChannelEnter()" runat="server">
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    
    <tr> 
        <td width="150px"  class="tdStyleBorderBK">Page</td> 
        <td><asp:TextBox ID="txtPage" width="150px"  onkeydown="txtPageEnter()" runat="server" Text="0"></asp:TextBox></td>
        <td>
           <asp:RequiredFieldValidator ID="valrTxtPage" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtPage" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
           <asp:CompareValidator ID="valcTxtPage" runat="server" 
                ErrorMessage="please input a Integer" ControlToValidate="txtPage" Type="Integer"
                ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
       </td>
    </tr>

    <tr> 
        <td width="150px"  class="tdStyleBorderBK">StartAddress</td> 
        <td><asp:TextBox ID="txtStartAddress" width="150px"  onkeydown="txtStartAddressEnter()" runat="server" Text="0"></asp:TextBox></td>
        <td>
           <asp:RequiredFieldValidator ID="valrTxtStartAddress" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtStartAddress" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
           <asp:CompareValidator ID="valcTxtStartAddress" runat="server" 
                ErrorMessage="please input a Integer" ControlToValidate="txtStartAddress" Type="Integer"
                ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
       </td>
    </tr>
    
    <tr> 
        <td width="150px"  class="tdStyleBorderBK">Length</td> 
        <td>            
            <asp:TextBox ID="txtLength" width="150px"  onkeydown="txtLengthEnter()" runat="server" Text="1"></asp:TextBox>
        </td>
        <td>
           <asp:RequiredFieldValidator ID="valrTxtLength" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtLength" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
           <asp:CompareValidator ID="valcTxtLength" runat="server" 
                ErrorMessage="please input a Integer" ControlToValidate="txtLength" Type="Integer"
                ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
       </td>
    </tr>
   
    <tr> 
        <td width="150px"  class="tdStyleBorderBK">Format</td> 
        <td>
            <asp:DropDownList ID="ddlFormat" width="155px"  onkeydown="ddlFormatEnter()" runat="server">
            <asp:ListItem>IEEE754</asp:ListItem>
            <asp:ListItem>U16</asp:ListItem>
            <asp:ListItem>U8</asp:ListItem>
            <asp:ListItem>U32</asp:ListItem>
            <asp:ListItem>String</asp:ListItem>
            <asp:ListItem>Double</asp:ListItem>
            <asp:ListItem>0</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>    
</table>
<hr />
