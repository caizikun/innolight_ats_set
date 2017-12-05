<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionButtons.ascx.cs" Inherits="ASCXOptionButtons" %>
<script type="text/javascript" >
    function CheckDelete() {
        if (confirm('Are you sure to delete?')) {
            return true;
        }
        else {
            return false;
        }
    }
    </script>

<style type="text/css">
    .style1 {
        width: 66px;
    }
</style>
<div>
<table>
<tr>
<td>
 <asp:Button ID="BtAdd" UseSubmitBehavior="false"  runat="server" Text="Add" Width="66px" 
        onclick="BtAdd_Click" CausesValidation="False" />
</td>
</tr>
<tr>
<td>
 <asp:Button ID="BtDelete" UseSubmitBehavior="false"  runat="server" Text="Delete" Width="66px" 
      OnClientClick=" if(!CheckDelete()){return false;}"
        onclick="BtDelete_Click" CausesValidation="False" />
</td>
</tr>
<tr>
<td>
 <asp:Button ID="BtCopy" UseSubmitBehavior="false"  runat="server" Text="Copy" Width="66px" 
         CausesValidation="False" onclick="BtCopy_Click" Visible="False" />
</td>
</tr>
<tr>
<td>
 <asp:Button ID="BtEdit" UseSubmitBehavior="false" runat="server" Text="Edit" Width="66px" 
        onclick="BtEdit_Click" CausesValidation="False" />        
</td>
</tr>
<tr>
<td>
 <asp:Button ID="BtSave" UseSubmitBehavior="false"  runat="server" AutoPostBack="false" Text="Save" Width="66px" 
        onclick="BtSave_Click"  />
        <%--<input type="button" id="BtSave" value="Save" 
        runat="server" class="style1" onclick="return BtSave_onclick()" />--%>
</td> 
</tr>
<tr>
<td>
 <asp:Button ID="BtCancel" UseSubmitBehavior="false"  runat="server" AutoPostBack="false" Text="Cancel" 
        Width="66px" onclick="BtCancel_Click" CausesValidation="False" 
          />
        <%--<input type="button" id="BtSave" value="Save" 
        runat="server" class="style1" onclick="return BtSave_onclick()" />--%>
</td> 
</tr>

</table>
</div>