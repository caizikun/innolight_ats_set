<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModelInfo.ascx.cs" Inherits="Frame_TestPlan_ModelInfo" %>
<script language="javascript" type="text/javascript">

    function ddlFailBreakEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=ddlIgnoreFlag.ClientID%>';
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

    function ddlIgnoreFlagEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {

            var tempt = '<%=ddlFailBreak.ClientID%>';
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
<div id="_ModelInfo">
    <table cellspacing="1" cellpadding="0">        
        <tr>
            <td width="150px" class="tdStyleBorderBK">名称</td>
            <td><asp:Label ID="lblItemName" Width="200px" height="26px" runat="server"  style="border:solid #CCCCCC 1px;line-height :31px;padding-left:5px;"></asp:Label></td>            
        </tr>
        <tr>
            <td width="150px" class="tdStyleBorderBK">是否跳过</td>
            <td><asp:DropDownList ID="ddlIgnoreFlag" width="207px" height="28px" onkeydown="ddlIgnoreFlagEnter()" runat="server">
                <asp:ListItem>true</asp:ListItem>
                <asp:listitem>false</asp:listitem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td width="150px" class="tdStyleBorderBK">出错是否停止</td>
            <td><asp:DropDownList ID="ddlFailBreak" width="207px" height="28px" onkeydown="ddlFailBreakEnter()" runat="server">
                <asp:ListItem>true</asp:ListItem>
                <asp:listitem>false</asp:listitem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td width="150px" class="tdStyleBorderBK">描述</td> 
            <td><asp:Label ID="lblItemDescription" Width="200px" height="26px" runat="server"
            style="border:solid #CCCCCC 1px;line-height :31px;padding-left:5px;"></asp:Label></td>
        </tr>        
    </table>
</div>