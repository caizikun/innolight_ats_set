<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="PNSelfInfor.aspx.cs" Inherits="ASPXPNSelfInfor" %>

<%@ Register src="~/Frame/OptionButtons.ascx" tagname="OptionButtons" tagprefix="uc1" %>
<%@ Register src="~/Frame/Production/PNSelfInfor.ascx" tagname="PNSelfInfor" tagprefix="uc2" %>

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
      <asp:Label ID="LabelList" runat="server" Text="产品信息"></asp:Label>
 </div>
  <div class="OptionButton">                           
       <uc1:optionbuttons ID="OptionButtons1" runat="server"/>                          
 </div> 
 </div>

<div id="Main" class="Main">
<%--<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
   </asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
  <ContentTemplate>--%>
  <table border="0" cellspacing="2" cellpadding="0" width="550px" style="float:left;" >        
    	<tr>
    		<td>                
             <asp:PlaceHolder ID="PNSelfInfor" Runat="Server" ></asp:PlaceHolder> 
            </td>
    	</tr>   
</table>

  <table id="tdStandard"  runat="server" border="1" cellspacing="1" cellpadding="0" style="font-family :微软雅黑; visibility:hidden;">     
  	    <tr style="height:30px;">
    		<td colspan="8" style="text-align:center;">                
                <asp:Label ID="Label7" runat="server" Text="产品命名规范"></asp:Label>         
            </td>
    	</tr>      
    	<tr style="height:27px;font-size:15px;">
    		<td style="width:135px;text-align:center;">                        
                <asp:Label ID="Label1" runat="server" Text="XX"></asp:Label>         
            </td>
            <td style="text-align:center;">                
                <asp:Label ID="Label2" runat="server" Text="_"></asp:Label>         
            </td>
            <td  style="width:100px;text-align:center;">                
                <asp:Label ID="Label3" runat="server" Text="X(N)"></asp:Label>         
            </td>
            <td style="text-align:center;">                
                <asp:Label ID="Label8" runat="server" Text="_"></asp:Label>         
            </td>
            <td style="width:10px;text-align:center;">                
                <asp:Label ID="Label9" runat="server" Text="G"></asp:Label>         
            </td>
            <td style="width:65px;text-align:center;">                 
                <asp:Label ID="Label10" runat="server" Text="X(N)"></asp:Label>         
            </td>
            <td style="text-align:center;">                
                <asp:Label ID="Label11" runat="server" Text="_"></asp:Label>         
            </td>
            <td style="width:95px;text-align:center;">               
                <asp:Label ID="Label12" runat="server" Text="X(N)"></asp:Label>         
            </td>
    	</tr>   
       	<tr style="height:90px; font-size:15px; ">
    		<td style="width:135px;">                
                <asp:Label ID="Label4" runat="server" Text="TR: 光模块" style="margin-left:2px;"></asp:Label> 
                <br/> 
                <asp:Label ID="Label18" runat="server" Text="TC: copper模块" style="margin-left:2px;"></asp:Label> 
                <br/> 
                <asp:Label ID="Label19" runat="server" Text="TF: AOC" style="margin-left:2px;"></asp:Label>     
                <br/> 
                <asp:Label ID="Label20" runat="server" Text="OE: OpticalEngine" style="margin-left:2px;"></asp:Label>           
            </td>
            <td>                
                <asp:Label ID="Label5" runat="server" Text="_" style="margin-left:5px;margin-right:5px;"></asp:Label>         
            </td>
            <td style="width:105px;">                    
                <asp:Label ID="Label6" runat="server" Text="产品命名，" style="margin-left:2px;"></asp:Label>        
                <br/> 
                <asp:Label ID="Label21" runat="server" Text="建议5个字以内" style="margin-left:2px;"></asp:Label> 
            </td>
            <td>           
                <asp:Label ID="Label13" runat="server" Text="_" style="margin-left:5px;margin-right:5px;"></asp:Label>         
            </td>
             <td>                  
                <asp:Label ID="Label14" runat="server" Text="G" style="margin-left:5px;margin-right:5px;"></asp:Label>         
            </td>
            <td style="width:65px;">                 
                <asp:Label ID="Label15" runat="server" Text="版本编号" style="margin-left:2px;"></asp:Label>         
            </td>
            <td>              
                <asp:Label ID="Label16" runat="server" Text="_" style="margin-left:5px;margin-right:5px;"></asp:Label>         
            </td>
            <td style="width:95px;">                    
                <asp:Label ID="Label17" runat="server" Text="产品方案描述" style="margin-left:2px;"></asp:Label>         
            </td>
    	</tr>   
</table>
<%--</ContentTemplate>
   </asp:UpdatePanel>--%>
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