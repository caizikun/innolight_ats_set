<%@ Page Title="" Language="C#" AutoEventWireup="true"  EnableEventValidation="false" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" >
    /**�и��������ڸ����ڴ�*/
    if (self != top) { top.location = self.location; }

    function EnterDB() {
        if (confirm('����������ݿ���')) {
            document.getElementById("lbDB").value = "1";    //����������ݿ�
        }
        else {
            document.getElementById("lbDB").value = "0";    //������ʽ���ݿ�
        }
        form1.submit();
    }

    function SyncDB() {
        if (confirm('����ͬ�����ݿ���Ϣҳ����')) {
            document.getElementById("lbDB").value = "2";    //����ͬ�����ݿ�ҳ��
        }
        else {
            if (confirm('����������ݿ���')) {
                document.getElementById("lbDB").value = "1";    //����������ݿ�
            }
            else {
                document.getElementById("lbDB").value = "0";    //������ʽ���ݿ�
            }
        }
        form1.submit();
    }
</script>  

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�û���¼</title>
    <link href="App_Themes/Site.css" rel="stylesheet" type="text/css" />
</head>

<body id ="body_login" >
    <form id="form1" runat="server">
    <div id="LoginBackground" >     
    </div>
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
			       <div id="user">�� ��
			         <!--<input type="text" name="textfield" />-->
                     <asp:TextBox ID="txtUserName" runat="server" Width="116px"></asp:TextBox>
			       </div>
				   <div id="password">��   ��
				     <!--<input type="password" name="textfield2" />-->
                       <asp:TextBox ID="txtPwd" runat="server" Width="115px" TextMode="Password"></asp:TextBox>
				   </div>
				   <!--<div id="btn"><a href="#">��¼</a><a href="#">���</a></div>-->
			       <div id="btn">
                       <asp:Button ID="btnLogin" runat="server" Height="24px"  onclick="btnLogin_Click" 
                           Text="��  ��" Width="61px" />
                       <input type="hidden" id="lbDB" runat="server" />
                   </div>                  
			  </div>
		 </div>
		 <div id="down" >
		      <div id="down_left">
			      <div id="inf">
                       <span class="inf_text">�汾��Ϣ</span>
					   <span class="copyright">ATS��Ϣ����ϵͳ V2.0</span>
			      </div>
			  </div>
		 </div>
         </ContentTemplate>
        </asp:UpdatePanel>
	</div>
    </form>
</body>
</html>