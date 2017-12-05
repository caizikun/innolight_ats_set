<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="SyncPage.aspx.cs" Inherits="SyncPage" %>

<%@ Register src="Frame/AccountInfo.ascx" tagname="AccountInfo" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/App_Themes/Site.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.11.2.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/RefreshTime.js") %>"></script>  
</head>
<body>
 <form id="Form1" defaultbutton="" runat="server" scroll="no">
 <div   id="primary"class="show" scroll="no">
    <div id="div_head" class="div_head">
        <div>          
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logo.png" />
        </div>               
        <div id="UserInfor">
           <uc1:AccountInfo ID="UserAccountInfo" runat="server" />      
        </div>               
    </div>

<div id="Middle2" style="background:#DDEDFA;">  
    <div style="width:230px; float:left;">
      <asp:Button ID="btSyncDB" runat="server" UseSubmitBehavior="false"  Text="同步数据库信息" class="btSyncDB" 
          Height="50px" Width="150px" Font-Bold="True" ForeColor="Black" Font-Underline="True" 
          style="font-family :微软雅黑;" onclick="btSyncDB_Click"/>     
    </div>

    <div style="width:600px; float:left; margin-top:16px;">
         <iframe src="SyncLog.aspx" id="OutputFrame" allowtransparency="true" frameborder="1"
                width="600px" scrolling="auto" marginheight="0" style="background-color:White;"></iframe>     
    </div> 

</div> 
<hr>
<div id="Middle" style=" background:#DDEDFA; width :100%;">
        <div style="width:200px;" >
              <asp:Button ID="btDebugDB" runat="server" 
                  Text="进入Debug数据库" class="btDebugDB"
                  Height="50px" Width="165px" Font-Bold="True" ForeColor="Black" 
                  style="font-family :微软雅黑;" Font-Underline="True" onclick="btDebugDB_Click" />
        </div>   
        <div style="width:200px;" >
              <asp:Button ID="btDB" runat="server" 
                  Text="进入正式数据库" class="btDebugDB"
                  Height="50px" Width="148px" Font-Bold="True" ForeColor="Black" 
                  style="font-family :微软雅黑;" Font-Underline="True" onclick="btDB_Click"  />
        </div>   
</div>
 
   
     
    <div id="Root">      
        <asp:Label ID="LabelRoot" runat="server" Text=""  style=" line-height :25px;"> 
         </asp:Label>  
    </div>
         
    <script type="text/javascript" language="JavaScript">
              var winWidth = 1170;
              var winHeight = 558;
              var winHeightMain = 498;
              function findDimensions() { //函数：获取尺寸
                  //通过深入Document内部对body进行检测，获取窗口大小
                  if (document.documentElement && document.documentElement.clientHeight && document.documentElement.clientWidth) {
                      winHeight = document.documentElement.clientHeight - 115;
                      winHeightMain = document.documentElement.clientHeight - 175;
                      winWidth = document.body.clientWidth - 200;
                  }
                  //设置div的具体宽度=窗口的宽度的%
                  if (document.getElementById("Middle")) {
                      document.getElementById("Middle").style.height = winHeight / 2 - 10 - 120 + "px";
                  }

                  if (document.getElementById("Middle2")) {
                      document.getElementById("Middle2").style.height = winHeight / 2 - 10 + 122 + "px";
                  }

                  if (document.getElementById("OutputFrame")) {
                      document.getElementById("OutputFrame").style.height = winHeight / 2 - 10 + 85 + "px";
                  }                                      
              }
              findDimensions();
              window.onresize = findDimensions;

   </script> 


 </div>
    </form>
</body>
</html>