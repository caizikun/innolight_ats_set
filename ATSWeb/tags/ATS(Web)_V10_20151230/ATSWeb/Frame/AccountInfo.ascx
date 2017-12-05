<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountInfo.ascx.cs" Inherits="userInfo" %>

<script type="text/javascript" >
    function CheckExit()
    {
        if (confirm('Are you sure to exit？'))
         {
            return true;
         }
        else 
        {
            return false;
        }
    }
    </script>
<style type="text/css">
    .style1
    {
        width: 136px;
    }
</style>


<img  id="userImg" src="../Images/user.png" alt="用户Logo"  runat="server"/><br />


<table  style=" position:relative; margin:0px auto">
<tr><td class="style1"> Welcome to login </td></tr>
<tr >
<td class="style1" >
<div id="hlkUserIDDIV" runat="server" class="hideUserName" title="">
<asp:HyperLink ID="hlkUserID" runat="server" Width="50px">User</asp:HyperLink>
</div>
</td>
<td>
<asp:Button ID="btnChangePWD" UseSubmitBehavior="false"  runat="server" Text="PW" style="height:20px;"   onclick="btnChangePWD_Click" Width="50px"/>
</td>
</tr>
<tr >
<td class="style1">
    <asp:Label ID="lblTime" runat="server" Width="50px"></asp:Label>
</td>
<td>
<asp:Button ID="btnExit" UseSubmitBehavior="false"  runat="server" Text="Exit" OnClientClick=" if(!CheckExit()){return false;}"  onclick="btnExit_Click" style="height:20px;"  Width="50px"/>
</td>
</tr>
</table>