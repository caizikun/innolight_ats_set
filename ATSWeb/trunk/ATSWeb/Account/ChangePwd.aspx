<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="Account_ChangePwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Headaspx" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/App_Themes/Site.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.11.2.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/RefreshTime.js") %>"></script>  
</head>

<body>
 <form id="formaspx" runat="server">
 <div id="Main" class="Main">
     <table id="ChangeFunc" class="showTableData" runat="Server">
        <tr>
            <td> * 原密码 </td> 
            <td> <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Width="148px"></asp:TextBox> </td>
        </tr>     

        <tr>
            <td> * 新密码 </td> 
            <td> <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" Width="148px"></asp:TextBox> </td>
            <td> 
            &nbsp;           
            <asp:RequiredFieldValidator ID="valrTxtNewPwd" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="txtNewPwd" ForeColor="Red"  
            SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtNewPwd" ForeColor="Red" 
            ID="valeTxtNewPwd" ValidationExpression="\S{6,50}" runat="server" ErrorMessage="密码长度6-50">
            </asp:RegularExpressionValidator>
            <asp:CompareValidator ID="valcTxtNewPwd" runat="server" 
            ErrorMessage="新密码与原密码相同!" ControlToValidate="txtNewPwd"
            ForeColor="Red" SetFocusOnError="True" ControlToCompare="txtPwd" Operator="NotEqual"></asp:CompareValidator>
            </td> 
        </tr>  

        <tr>
            <td> * 确认密码 </td> 
            <td> <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password" Width="148px"></asp:TextBox> </td>
            <td>
             &nbsp;  
                <asp:RequiredFieldValidator ID="valrTxtConfirmPwd" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtConfirmPwd" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="txtConfirmPwd" ForeColor="Red" 
                ID="valeTxtConfirmPwd" ValidationExpression="\S{6,50}" runat="server" ErrorMessage="密码长度6-50">
                </asp:RegularExpressionValidator>
                <asp:CompareValidator ID="valcTxtConfirmPwd" runat="server" 
                ErrorMessage="密码前后输入不一致！" ControlToCompare="txtNewPwd" 
                ForeColor="Red" ControlToValidate="txtConfirmPwd"></asp:CompareValidator>
            </td> 
        </tr>  
        

        <tr>
            <td></td> 
            <td align="right" > 
                <asp:Button ID="btnOK" runat="server" Text="确定" onclick="btnOK_Click"/> </td>
        </tr>

        <tr>
            <td></td> 
            <td> 
                <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
            </td>
        </tr> 
     </table>
</div>  

     <script type="text/javascript" language="JavaScript">
         var winHeight = 558;
         function findDimensions() {                  //函数：获取尺寸
             if (document.documentElement && document.documentElement.clientHeight) {
                 winHeight = document.documentElement.clientHeight;
             }
             if (document.getElementById("Main")) {
                 document.getElementById("Main").style.height = winHeight + "px";
             }
         }
         findDimensions();
         window.onresize = findDimensions;

         //         window.onload = function () {
         //             document.getElementById("waitImg").style.display = "none";
         //         }
   </script> 
 </form>
</body>
</html>

