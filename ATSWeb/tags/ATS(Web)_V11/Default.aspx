<%@ Page Title="" Language="C#" AutoEventWireup="true"  EnableEventValidation="false" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>用户登录</title>
    <link href="App_Themes/Site.css" rel="stylesheet" type="text/css" />
</head>

<body id ="body_login" >
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
      <ContentTemplate>
    <div id="login" class="login">
	
	     <div id="top">
		      <div id="top_left"><img src="Images/login_03.gif" alt="" /></div>
			  <div id="top_center" class="password"></div>
		 </div>
		 
		 <div id="center"/>
		      <div id="center_left"></div>
			  <div id="center_middle">
			       <div id="user">用 户
			         <!--<input type="text" name="textfield" />-->
                     <asp:TextBox ID="txtUserName" runat="server" Width="116px"></asp:TextBox>
			       </div>
				   <div id="password">密   码
				     <!--<input type="password" name="textfield2" />-->
                       <asp:TextBox ID="txtPwd" runat="server" Width="115px" TextMode="Password"></asp:TextBox>
				   </div>
				   <!--<div id="btn"><a href="#">登录</a><a href="#">清空</a></div>-->
			       <div id="btn">
                       <asp:Button ID="btnLogin" runat="server" Height="24px"  onclick="btnLogin_Click" 
                           Text="登  入" Width="61px" />
                   </div>
			  </div>
		 </div>
		 <div id="down" >
		      <div id="down_left">
			      <div id="inf">
                       <span class="inf_text">版本信息</span>
					   <span class="copyright">ATS管理信息系统 2015 v1.0</span>
			      </div>
			  </div>
		 </div>
         </ContentTemplate>
        </asp:UpdatePanel>
	</div>
    </form>
</body>
</html>