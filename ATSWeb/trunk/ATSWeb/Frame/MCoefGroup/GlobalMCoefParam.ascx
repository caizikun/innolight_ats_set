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

            var tempt = '<%=txtAmplify.ClientID%>';
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
    function txtAmplifyEnter() {
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
<table cellspacing="1" cellpadding="0">

    <tr> 
        <td width="150px" class="tdStyleBorderBK">类型</td> 
        <td><asp:DropDownList ID="ddlItemType" width="204px" height="28px" 
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
        <td width="150px" class="tdStyleBorderBK">名称</td> 
        <td style="position:relative;">
        <asp:DropDownList ID="ddlItem" runat="server" width="204px" height="28px" AutoPostBack="True" 
                onselectedindexchanged="ddlItem_SelectedIndexChanged" onkeydown="ddlItemEnter()">
            </asp:DropDownList>
        <asp:TextBox ID="txtItemName" width="179px" height="22px" onkeydown="txtItemNameEnter()" runat="server" style="position:absolute; margin-left:-208px;"></asp:TextBox>        
        </td>
        <td>
         &nbsp;
            <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtItemName" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" Display="Dynamic"
            ID="valeTxtItemName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>           
       </td>
    </tr>
    
    <tr> 
        <td width="150px"  class="tdStyleBorderBK">通道</td> 
        <td>
            <asp:DropDownList ID="ddlChannel" width="204px" height="28px" onkeydown="ddlChannelEnter()" runat="server">
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    
    <tr> 
        <td width="150px"  class="tdStyleBorderBK">页数</td> 
        <td><asp:TextBox ID="txtPage" width="200px" height="22px"  onkeydown="txtPageEnter()" runat="server" Text="0"></asp:TextBox></td>
        <td>
         &nbsp;
           <asp:RequiredFieldValidator ID="valrTxtPage" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtPage" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
           <asp:CompareValidator ID="valcTxtPage" runat="server" 
                ErrorMessage="请输入整数" ControlToValidate="txtPage" Type="Integer" Display="Dynamic"
                ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
       </td>
    </tr>

    <tr> 
        <td width="150px"  class="tdStyleBorderBK">开始地址</td> 
        <td><asp:TextBox ID="txtStartAddress" width="200px" height="22px"  onkeydown="txtStartAddressEnter()" runat="server" Text="0"></asp:TextBox></td>
        <td>
         &nbsp;
           <asp:RequiredFieldValidator ID="valrTxtStartAddress" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtStartAddress" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
           <asp:CompareValidator ID="valcTxtStartAddress" runat="server" 
                ErrorMessage="请输入整数" ControlToValidate="txtStartAddress" Type="Integer" Display="Dynamic"
                ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
       </td>
    </tr>
    
    <tr> 
        <td width="150px"  class="tdStyleBorderBK">长度</td> 
        <td>            
            <asp:TextBox ID="txtLength" width="200px" height="22px" onkeydown="txtLengthEnter()" runat="server" Text="1"></asp:TextBox>
        </td>
        <td>
         &nbsp;
           <asp:RequiredFieldValidator ID="valrTxtLength" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtLength" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
           <asp:CompareValidator ID="valcTxtLength" runat="server" 
                ErrorMessage="请输入整数" ControlToValidate="txtLength" Type="Integer" Display="Dynamic"
                ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
       </td>
    </tr>
   
    <tr> 
        <td width="150px"  class="tdStyleBorderBK">格式</td> 
        <td>
            <asp:DropDownList ID="ddlFormat" width="204px" height="28px"  onkeydown="ddlFormatEnter()" runat="server">
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
    
    <tr> 
        <td width="150px"  class="tdStyleBorderBK">放大倍数</td> 
        <td>            
            <asp:TextBox ID="txtAmplify" width="200px" height="22px" onkeydown="txtAmplifyEnter()" runat="server" Text="1"></asp:TextBox>
        </td>
        <td>
         &nbsp;
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtAmplify" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
           <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ErrorMessage="请输入浮点数" ControlToValidate="txtAmplify" Type="Double" Display="Dynamic"
                ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
       </td>
    </tr>  
</table>
