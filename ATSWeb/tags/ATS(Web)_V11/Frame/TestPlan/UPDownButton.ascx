<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UPDownButton.ascx.cs" Inherits="Frame_TestPlan_UPDownButton" %>
<table  width="100%">
	<tr class="tdCenter">
		<td class="tdCenter">
            <asp:Button ID="ButtonUP"  UseSubmitBehavior="false" runat="server" Text="∧" onclick="ButtonUP_Click" 
           
                Width="40px" ForeColor="#0099CC" Height="15px" />               
            </td>
        
	</tr>
   
    <tr class="tdCenter">		
        <td class="tdCenter">
           <asp:Button ID="ButtonDown" UseSubmitBehavior="false"  runat="server" Text="∨" onclick="ButtonDown_Click" 
          
                Width="40px" ForeColor="#0099CC" Height="15px" /> 
            </td>
	</tr>
</table>