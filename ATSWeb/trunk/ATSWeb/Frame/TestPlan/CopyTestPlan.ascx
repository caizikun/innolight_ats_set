<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CopyTestPlan.ascx.cs" Inherits="ASCXCopyTestPlan" %>

<style type="text/css">
    .style2
    {
        height: 10px;
    }
</style>

<script language="javascript" type="text/javascript">
    function TBItemNameEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {
            var tempt = '<%=TBItemName.ClientID%>';
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
        <td width="150px" class="tdStyleBorderBK">
            <asp:Label ID="TH2" runat="server" Text="名称"></asp:Label>      
        </td> 
        <td><asp:TextBox ID="TBItemName" width="200px" height="22px" onkeydown="TBItemNameEnter()" runat="server" MaxLength="30"></asp:TextBox></td>
        <td>
        &nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="TBItemName" ForeColor="Red" 
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ControlToValidate="TBItemName" ForeColor="Red" Display="Dynamic"
            ID="RegularExpressionValidator3" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ErrorMessage="请输入字符串" ControlToValidate="TBItemName" Display="Dynamic"
                Operator="NotEqual" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator>
        </td>
        
    </tr>
</table>

<table>
<tr>
<td class="style2">
</td>
</tr>
<tr>
<td >
    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
        oncheckedchanged="CheckBox1_CheckedChanged" />
</td>
<td>    
    <asp:Label ID="Label1" runat="server" Text="复制选中测试方案至其他品名下？"></asp:Label>    
</td>
</tr>
</table>

<asp:Panel ID="Panel1" runat="server" Height="59px" Width="500px" 
    Visible="False">
<table  cellspacing="1" cellpadding="0" style="width: 451px;">
<tr>
 <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="Label3" runat="server" Text="产品类型"></asp:Label>
</td>
<td>    
    <asp:DropDownList ID="DropDownList1" runat="server" width="204px" height="28px" 
        AutoPostBack="True" ontextchanged="DropDownList1_TextChanged" 
        >
    </asp:DropDownList>  
    &nbsp;  
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="DropDownList1" ErrorMessage="不能为空" ForeColor="Red" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
 <td width="150px" class="tdStyleBorderBK">
    <asp:Label ID="Label4" runat="server" Text="品名"></asp:Label>   
</td>
<td>   
    <asp:DropDownList ID="DropDownList2" runat="server" width="204px" height="28px">
    </asp:DropDownList>   
    &nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="DropDownList2" ErrorMessage="不能为空" ForeColor="Red" 
        SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
</td>
</tr>
</table>
</asp:Panel>


