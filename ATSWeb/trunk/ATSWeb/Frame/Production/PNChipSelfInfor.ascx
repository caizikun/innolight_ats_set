<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PNChipSelfInfor.ascx.cs" Inherits="Frame_Production_PNChipSelfInfor" %>
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
<table cellspacing="1" cellpadding="0" >
<tr>   
   <td width="150px" class="tdStyleBorderBK">
   <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
   </td>
   <td>       
       <asp:DropDownList ID="Colum1Text" width="204px" height="28px" onkeydown="Colum1TextEnter()" runat="server">
       
        </asp:DropDownList>
   </td>
  
</tr>
<tr>
    
   <td width="150px" class="tdStyleBorderBK">
   <asp:Label ID="TH2" runat="server" Text="类型"></asp:Label>
   </td>
   <td>       
       <asp:DropDownList ID="Colum2Text" width="204px" height="28px" onkeydown="Colum2TextEnter()" runat="server">
        <asp:ListItem>LDD</asp:ListItem>
        <asp:ListItem>AMP</asp:ListItem>   
        <asp:ListItem>DAC </asp:ListItem>  
        <asp:ListItem>CDR</asp:ListItem>  
        </asp:DropDownList>
   </td>
  
</tr>

<tr>
    <td width="150px" class="tdStyleBorderBK">
        <asp:Label ID="TH3" runat="server" Text="方向"></asp:Label>
    </td>
    <td>     
        <asp:DropDownList ID="Colum3Text" width="204px" height="28px" onkeydown="Colum3TextEnter()" runat="server">
        <asp:ListItem>Tx</asp:ListItem>
        <asp:ListItem>Rx</asp:ListItem>
        <asp:ListItem>Tx&Rx</asp:ListItem>    
        </asp:DropDownList>
    </td>   
</tr>
</table>
</div>