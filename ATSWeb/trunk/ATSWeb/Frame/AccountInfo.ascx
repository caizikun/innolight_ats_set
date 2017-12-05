<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountInfo.ascx.cs" Inherits="userInfo" %>

<script type="text/javascript" >
    function CheckExit()
    {
        if (confirm('确定要退出？'))
         {
            return true;
         }
        else 
        {
            return false;
        }
    }

    function btnChangePWD_Click() {
        var iframe = document.getElementById("context");
        iframe.src = "Account/ChangePwd.aspx";
    }   
    </script>

<div id="hlkUserIDDIV" runat="server">
 <table>
 <tr>
 <td valign="bottom"   >
      <img id="userImg" src="../Images/user.png" alt="用户Logo"  runat="server" />
 </td>
  <td valign="middle">
      <asp:Label ID="LabelUser" runat="server" Text="当前用户："
        style="font-family :微软雅黑;" ForeColor="white"></asp:Label>
 </td>
  <td valign="middle">
      <asp:HyperLink ID="hlkUserID" class="hideUserName" runat="server" Width="70px" style=" font-family :微软雅黑;">User</asp:HyperLink>
 </td>
  <td valign="bottom">
      <asp:ImageButton ID="btnChangePWD" onClientClick="btnChangePWD_Click();return false;" runat="server"
         ImageUrl="~/Images/password.png"  ToolTip="修改密码" alt="[修改密码]"/>
 </td>
 <td valign="middle">
   <asp:Button ID="btnExit" class="btExit" runat="server" 
         OnClientClick=" if(!CheckExit()){return false;}"  onclick="btnExit_Click"
      style="position:relative;margin-left:8px;" ToolTip="退出" Height="16px" 
         Width="16px" UseSubmitBehavior="False"/>
 </td>
 </tr>
 </table>               
</div>


