﻿<%@ Page Title="" Language="C#" AutoEventWireup="true"  EnableEventValidation="false" CodeFile="ReportHeaderSpecsList.aspx.cs" Inherits="ASPXReportHeaderSpecsList" %>

<%@ Register src="~/Frame/OptionButtons.ascx" tagname="OptionButtons" tagprefix="uc3" %>
<%@ Register src="~/Frame/TestReport/ReportHeaderSpecsList.ascx" tagname="ReportHeaderSpecsList" tagprefix="uc4" %>

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
      <asp:Label ID="LabelList" runat="server" Text="表头规格项列表"></asp:Label>
 </div>
  <div class="OptionButton">                            
                <uc3:OptionButtons ID="OptionButtons1" runat="server" />                             
 </div>
 </div>

<div id="Main" class="Main"> 
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
   </asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                <ContentTemplate>
<table id="tableFC" border="0" cellspacing="0" runat="server"> 
    	<tr valign="middle">
    		    <td id="EqContent" >            
                 <asp:PlaceHolder ID="plhMain" Runat="Server" ></asp:PlaceHolder> 
                </td>
    	    </tr> 
            <tr id="trFC"  visible="false" runat="server">
            <td ></td>
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
