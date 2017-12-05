<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="FlowContrlSelfInfor.aspx.cs" Inherits="ASPXFlowContrlSelfInfor" %>

<%@ Register src="~/Frame/TestPlan/FlowControlSelfInfor.ascx" tagname="FlowControlSelfInfor" tagprefix="uc1" %>
<%@ Register src="~/Frame/OptionButtons.ascx" tagname="OptionButtons" tagprefix="uc2" %>

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

<body onkeydown="if (event.keyCode == 116){window.location.reload();return false;}">
 <form id="formaspx" runat="server">
 <div id="Navi" class="Navi">
  <div class="NaviList" >
         <asp:PlaceHolder ID="plhNavi"  Runat="Server" ></asp:PlaceHolder> 
</div> 
</div> 

<div id="Options" class="Options">
 <div class="OptionLog" >
     <asp:Image ID="ImageList" runat="server" ImageUrl="~/Images/selfInfo.gif" 
         Height="20px" />                      
 </div>
 <div class="OptionLabel" >              
      <asp:Label ID="LabelList" runat="server" Text="流程控制信息"></asp:Label>
 </div>
  <div class="OptionButton">                            
      <uc2:OptionButtons ID="OptionButtons1" runat="server" />                           
 </div> 
 </div>

<div id="Main" class="Main">
<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
   </asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
  <ContentTemplate>
  <table border="0" cellspacing="2" cellpadding="0" width="100%" >        
    	<tr>
    		<td>             
             <asp:PlaceHolder ID="FlowControSelfInfor" Runat="Server" ></asp:PlaceHolder> 
            </td>
    	</tr>   
</table>
</ContentTemplate>
   </asp:UpdatePanel>
</div>

    <script type="text/javascript" language="JavaScript">
        var winHeight = 558;
        function findDimensions() {                  //函数：获取尺寸
            if (document.documentElement && document.documentElement.clientHeight) {
                winHeight = document.documentElement.clientHeight - 60;
            }
            if (document.getElementById("Main")) {
                document.getElementById("Main").style.height = winHeight + "px";
            }
        }
        findDimensions();
        window.onresize = findDimensions;

   </script> 
 </form>
</body>
</html>


