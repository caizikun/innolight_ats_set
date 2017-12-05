<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChannelMapSelf.ascx.cs" Inherits="Frame_Production_ChannelMapSelf" %>
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
<table style=" border:solid #000 1px">



<tr>
    
   <td width="150px" class="tdStyleBorderBK">
   <asp:Label ID="TH1" runat="server" Text="ModuleLine"></asp:Label>
   </td>
   <td>       
       <asp:DropDownList ID="Colum1Text" width="205px" onkeydown="Colum1TextEnter()" runat="server">
       
        </asp:DropDownList>
   </td>
  
</tr>
<tr>
    
   <td width="150px" class="tdStyleBorderBK">
   <asp:Label ID="TH2" runat="server" Text="ChipLine"></asp:Label>
   </td>
   <td>       
       <asp:DropDownList ID="Colum2Text" width="205px" onkeydown="Colum2TextEnter()" runat="server">
       
        </asp:DropDownList>
   </td>
  
</tr>
<tr>
    
   <td width="150px" class="tdStyleBorderBK">
   <asp:Label ID="TH3" runat="server" Text="DebugLine"></asp:Label>
   </td>
   <td>       
       <asp:DropDownList ID="Colum3Text" width="205px" onkeydown="Colum3TextEnter()" runat="server">
       
        </asp:DropDownList>
   </td>
  
</tr>

</table>
</div>