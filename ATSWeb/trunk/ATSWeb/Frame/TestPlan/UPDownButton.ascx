<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UPDownButton.ascx.cs" Inherits="Frame_TestPlan_UPDownButton" %>
<div class="UPDown" >
            <asp:ImageButton ID="ButtonUP" runat="server" onclick="ButtonUP_Click" 
                ImageUrl="~/Images/up.png" Width="40px" Height="8px" AlternateText="UP" 
                ToolTip="Up"/>                             
</div>  
<div>
        <asp:ImageButton ID="ButtonDown" runat="server" onclick="ButtonDown_Click" 
                ImageUrl="~/Images/down.png" Width="40px" Height="8px" 
                AlternateText="Down" ToolTip="Down"/>
</div> 