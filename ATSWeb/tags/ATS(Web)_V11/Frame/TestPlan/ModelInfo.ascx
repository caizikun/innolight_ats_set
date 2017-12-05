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
    <table style=" border:solid #000 1px">        
        <tr>
            <td width="150px" class="tdStyleBorderBK">ModelName</td>
            <td><asp:Label ID="lblItemName" Width="200px" runat="server"></asp:Label></td>            
        </tr>
        <tr>
            <td width="150px" class="tdStyleBorderBK">Skip?</td>
            <td><asp:DropDownList ID="ddlIgnoreFlag" Width="205px" onkeydown="ddlIgnoreFlagEnter()" runat="server">
                <asp:ListItem>true</asp:ListItem>
                <asp:listitem>false</asp:listitem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td width="150px" class="tdStyleBorderBK">FailBreak?</td>
            <td><asp:DropDownList ID="ddlFailBreak" Width="205px" onkeydown="ddlFailBreakEnter()" runat="server">
                <asp:ListItem>true</asp:ListItem>
                <asp:listitem>false</asp:listitem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td width="150px" class="tdStyleBorderBK">Description</td> 
            <td><asp:Label ID="lblItemDescription" Width="200px" runat="server"></asp:Label></td>
        </tr>        
    </table>
</div>