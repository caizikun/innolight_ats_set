<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionButtons.ascx.cs" Inherits="ASCXOptionButtons" %>
<script type="text/javascript" >
    function CheckDelete() {
        if (confirm('确定删除吗?')) {
            return true;
        }
        else {
            return false;
        }
    }
    </script>

<div>
<table>
    
<tr>
<td id="Add" width="70px" runat="server">
     <asp:Button ID="BtAdd" UseSubmitBehavior="false"  runat="server" Text="" Height="18px" Width="52px" class="btAdd"
        onclick="BtAdd_Click" CausesValidation="False" ToolTip="add" />
</td>
<td id="Delete" width="63px" runat="server">
 <asp:Button ID="BtDelete" UseSubmitBehavior="false"  runat="server" Text="" Height="15px" Width="46px" class="btDelete"
      OnClientClick=" if(!CheckDelete()){return false;}"
        onclick="BtDelete_Click" CausesValidation="False" ToolTip="delete"  />
</td>
<td id="Copy" width="0px" runat="server">
 <asp:Button ID="BtCopy" UseSubmitBehavior="false"  runat="server" Text="" Height="18px" Width="52px" class="btCopy" 
         CausesValidation="False" onclick="BtCopy_Click" Visible="False" ToolTip="copy"/>
</td>
<td id="Edit" width="65px" runat="server">
 <asp:Button ID="BtEdit" UseSubmitBehavior="false" runat="server" Text="" Height="16px" Width="51px" class="btEdit"
        onclick="BtEdit_Click" CausesValidation="False" ToolTip="edit"/>    
</td>
<td id="Order" width="0px" runat="server">
 <asp:Button ID="BtOrder" UseSubmitBehavior="false"  runat="server" Text="排序" Height="18px" Width="65px" class="btOrder" 
         CausesValidation="False" onclick="BtOrder_Click" ToolTip="order" Visible="False" Font-Size="14px" Font-Bold="true"  />
</td>
<td id="OrderUp" width="0px" runat="server">
    <asp:Button ID="BtOrderUp" UseSubmitBehavior="false" runat="server" Text="上移" class="btOrderUp"
        CausesValidation="False" onclick="BtOrderUp_Click" Height="18px" Width="65px" ToolTip="Up" Visible="False" Font-Size="14px" Font-Bold="true"/>
</td>
<td  id="OrderDown" width="0px" runat="server">
<asp:Button ID="BtOrderDown" UseSubmitBehavior="false" runat="server" Text="下移" class="btOrderDown"
        CausesValidation="False" onclick="BtOrderDown_Click" Height="18px" Width="65px" ToolTip="Down" Visible="False" Font-Size="14px" Font-Bold="true"/>
</td>
<td id="Save" width="68px" runat="server">
 <asp:Button ID="BtSave" UseSubmitBehavior="false"  runat="server" AutoPostBack="false" Text="" Height="18px" Width="53px" class="btSave"                
      onclick="BtSave_Click" ToolTip="save" />
</td> 
<td id="Cancel" width="68px" runat="server">
 <asp:Button ID="BtCancel" UseSubmitBehavior="false"  runat="server" AutoPostBack="false" Text="" Height="16px" Width="53px" class="btCancel"  
      onclick="BtCancel_Click" CausesValidation="False" ToolTip="cancel" />
</td> 

</tr>

</table>
</div>