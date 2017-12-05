<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Query.aspx.cs" Inherits="WebFiles_TestReport_Query" %>

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
<div id="waitImg" class="wait" style="position:absolute; top:5px; height:20px; width:100px; right:10px;display:none;"></div>
 <div id="Navi" class="Navi">
  <div class="NaviList" >
         <asp:PlaceHolder ID="plhNavi"  Runat="Server" onprerender="plhNavi_PreRender" ></asp:PlaceHolder> 
 </div>
 </div>

<div id="Main" class="Main">
    <div>
    </div>
    
    <div>
        <table>
            <tr>
                <td>&nbsp;&nbsp;</td>
                
                <td>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1" Width="660px">
                    <asp:Label ID="Label1" runat="server" Text="SN:" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px" Width="30px"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" Width="614px" Height="22px" ></asp:TextBox>
                    
                </asp:Panel>
                </td>
                <td>
                   <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="" Height="28px" Width="90px" class="btQuery"
                        OnClientClick="javascript:document.getElementById('waitImg').style.display='block';"       
                        UseSubmitBehavior="False" />
                </td>

            </tr>
<%--            <tr>
            <td>
            </td>
            </tr>--%>
        </table>
    </div>
    
    <div id="gridview">
        <table style="margin-top: 0px">
            <tr>
                <td class="style2">&nbsp;&nbsp;</td>
                
                <td>
        <asp:GridView ID="GridView1"  CssClass="Grid" runat="server" Width="850px"
                        EnableViewState="False" 
                        onrowcommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" 
                        AutoGenerateColumns="False" CellPadding="4">           
            <Columns>
            <asp:BoundField DataField="SN" HeaderText="SN"/>
            <asp:BoundField DataField="TestPlan" HeaderText="测试方案" />
            <asp:BoundField DataField="StartTime" HeaderText="开始时间" />
            <asp:BoundField DataField="EndTime" HeaderText="结束时间" />
            <asp:BoundField DataField="Result" HeaderText="测试结果"/>
            <asp:TemplateField HeaderText="测试数据" ItemStyle-Width="106px">
            <ItemTemplate>
            <asp:LinkButton  ID="TestControllLinkButton"  runat="server" CommandName="TestControll" Width="106px" style="text-align:center; font-size: 16px;">查看
            </asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <Columns>
            <asp:TemplateField HeaderText="备份系数" ItemStyle-Width="84px">
            <ItemTemplate>
            <asp:LinkButton  ID="BackCoefLinkButton"  runat="server" CommandName="BackCoef" Width="84px" style="text-align:center;">查看
            </asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="ID" >
                    <ItemStyle  CssClass="hide" BorderColor="#507CD1" />
                    <HeaderStyle CssClass="hide" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle  CssClass="tHStyleCenterBottom" Wrap="False"/>
            <RowStyle Height="29px"/>
        </asp:GridView>
        </td>
            </tr>
        </table>
    </div>
    
    <div>
    </div>
</div>  

     <script type="text/javascript" language="JavaScript">
         var winHeight = 558;
         function findDimensions() {                  //函数：获取尺寸
             if (document.documentElement && document.documentElement.clientHeight) {
                 winHeight = document.documentElement.clientHeight - 30;
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

