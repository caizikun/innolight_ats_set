<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="AddNewTestPlan.aspx.cs" Inherits="ASPXAddNewTestPlan" %>

<%@ Register src="~/Frame/TestPlan/TestPlanInfor.ascx" tagname="TestPlanInfor" tagprefix="uc1" %>
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
      <asp:Label ID="LabelList" runat="server" Text="测试方案信息"></asp:Label>
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
                 <table border="0" cellspacing="2" cellpadding="0" width="545px" style="float:left;">       
    	<tr>
    		<td>            
             <asp:PlaceHolder ID="AddNewTestPlan" Runat="Server" ></asp:PlaceHolder> 
            </td>
    	</tr>   
</table>
</ContentTemplate>
   </asp:UpdatePanel>

       <table id="tdStandard"  runat="server" border="1" cellspacing="1" cellpadding="0" style="font-family :微软雅黑; visibility:visible;">     
  	    <tr style="height:30px;">
    		<td colspan="9" style="text-align:center;">                
                <asp:Label ID="Label7" runat="server" Text="测试方案命名规范"></asp:Label>         
            </td>
    	</tr>      
    	<tr style="height:27px;font-size:15px;">
    		<td style="width:102px;text-align:center;">                        
                <asp:Label ID="Label1" runat="server" Text="XXX"></asp:Label>         
            </td>
            <td style="text-align:center;">                
                <asp:Label ID="Label2" runat="server" Text="_"></asp:Label>         
            </td>
            <td  style="width:100px;text-align:center;">                
                <asp:Label ID="Label3" runat="server" Text="XX"></asp:Label>         
            </td>
            <td style="text-align:center;">                
                <asp:Label ID="Label8" runat="server" Text="_"></asp:Label>         
            </td>
            <td style="width:99px;text-align:center;">                
                <asp:Label ID="Label9" runat="server" Text="X(N)"></asp:Label>         
            </td>
            <td style="text-align:center;">                
                <asp:Label ID="Label10" runat="server" Text="_"></asp:Label>         
            </td>
            <td style="width:94px;text-align:center;">               
                <asp:Label ID="Label28" runat="server" Text="X"></asp:Label>         
            </td>
            <td style="text-align:center;">                
                <asp:Label ID="Label11" runat="server" Text="_"></asp:Label>         
            </td>
            <td style="width:38px;text-align:center;">               
                <asp:Label ID="Label12" runat="server" Text="X(N)"></asp:Label>         
            </td>
    	</tr>   
       	<tr style="height:140px; font-size:15px; ">
    		<td style="width:102px;">                
                <asp:Label ID="Label4" runat="server" Text="DEV: 产品调试" style="margin-left:2px;"></asp:Label> 
                <br/> 
                <asp:Label ID="Label18" runat="server" Text="DVT: DVT" style="margin-left:2px;"></asp:Label> 
                <br/> 
                <asp:Label ID="Label19" runat="server" Text="PRD: 生产" style="margin-left:2px;"></asp:Label>     
                <br/> 
                <asp:Label ID="Label20" runat="server" Text="PQT: PQT" style="margin-left:2px;"></asp:Label>           
            </td>
            <td>                
                <asp:Label ID="Label5" runat="server" Text="_" style="margin-left:2px;margin-right:2px;"></asp:Label>         
            </td>
            <td style="width:100px;">                    
                <asp:Label ID="Label6" runat="server" Text="TX: 测TX" style="margin-left:2px;"></asp:Label>        
                <br/> 
                <asp:Label ID="Label21" runat="server" Text="RX: 测RX" style="margin-left:2px;"></asp:Label> 
                 <br/> 
                <asp:Label ID="Label22" runat="server" Text="TR: 测TX&RX" style="margin-left:2px;"></asp:Label> 
            </td>
            <td>           
                <asp:Label ID="Label13" runat="server" Text="_" style="margin-left:2px;margin-right:2px;"></asp:Label>         
            </td>
            <td style="width:99px;">                  
                <asp:Label ID="Label14" runat="server" Text="FUL: LP+FMT" style="margin-left:2px;"></asp:Label>
                 <br/> 
                <asp:Label ID="Label23" runat="server" Text="LP: LP" style="margin-left:2px;"></asp:Label> 
                 <br/> 
                <asp:Label ID="Label24" runat="server" Text="FMT: FMT" style="margin-left:2px;"></asp:Label> 
                 <br/> 
                <asp:Label ID="Label25" runat="server" Text="3V3T: 3V3T" style="margin-left:2px;"></asp:Label> 
                 <br/> 
                <asp:Label ID="Label26" runat="server" Text="TSWAP: 温循" style="margin-left:2px;"></asp:Label> 
                 <br/> 
                <asp:Label ID="Label27" runat="server" Text="FQC: FQC" style="margin-left:2px;"></asp:Label>          
            </td>
            <td>                
                <asp:Label ID="Label15" runat="server" Text="_" style="margin-left:2px;margin-right:2px;"></asp:Label>         
            </td>
             <td style="width:94px;">                    
                <asp:Label ID="Label29" runat="server" Text="P: 生产机台" style="margin-left:2px;"></asp:Label>         
                <br/>
                <asp:Label ID="Label30" runat="server" Text="V: 实验机台" style="margin-left:2px;"></asp:Label>     
                <br/>
                <asp:Label ID="Label31" runat="server" Text="S: ODVT机台" style="margin-left:2px;"></asp:Label> 
            </td>
            <td>              
                <asp:Label ID="Label16" runat="server" Text="_" style="margin-left:2px;margin-right:2px;"></asp:Label>         
            </td>
            <td style="width:38px;">                    
                <asp:Label ID="Label17" runat="server" Text="附加" style="margin-left:3px;"></asp:Label>    
                <br/>     
                <asp:Label ID="Label32" runat="server" Text="描述" style="margin-left:3px;"></asp:Label>  
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

   </script> 
 </form>
</body>
</html>


