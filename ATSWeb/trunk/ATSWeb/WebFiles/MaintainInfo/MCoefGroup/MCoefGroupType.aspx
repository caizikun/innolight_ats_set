<%@ Page Title="" Language="C#"  AutoEventWireup="true" EnableEventValidation="false" CodeFile="MCoefGroupType.aspx.cs" Inherits="WebFiles_MCoefGroup_MCoefGroupType" %>

<%@ Register src="~/Frame/TestPlan/TestplanType.ascx" tagname="TestplanType" tagprefix="uc1" %>

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
     <asp:Image ID="ImageList" runat="server" ImageUrl="~/Images/List.gif" 
         Height="20px" />                      
 </div>
 <div class="OptionLabel" >              
      <asp:Label ID="LabelList" runat="server" Text="模块类型列表"></asp:Label>
 </div>
 </div>

<div id="Main" class="Main">
<table border="0" cellspacing="0">    
    	<tr valign="middle">
    		<td id="ModuleTypeContent" >
             <asp:PlaceHolder ID="plhMain" Runat="Server" ></asp:PlaceHolder>  
            </td>
    	</tr>
    </table> 
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

         function A() {
             parent.window.addTreeNode();
         }
   </script> 
 </form>
</body>
</html>

