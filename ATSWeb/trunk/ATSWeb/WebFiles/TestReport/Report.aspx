<%@ Page Title="" Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="WebFiles_TestReport_Report" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Headaspx" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/App_Themes/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/App_Themes/jquery.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="~/App_Themes/style.css" rel="stylesheet" type="text/css" />
    <link href="~/App_Themes/jquery-ui.css" rel="stylesheet" type="text/css" />
   <%-- <link rel="stylesheet" type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/ui-lightness/jquery-ui.css"/>--%>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.ui.core.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.ui.widget-1.8.1.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.multiselect.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jjquery-1.4.2.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/RefreshTime.js") %>"></script>  

    <script type="text/javascript">

        var valuestr;
        var flag = 0;

        $(function () {
            $('#sela').multiselect({
                noneSelectedText: "==请选择==",
                checkAllText: "全选",
                uncheckAllText: '不选',
                selectedList: 1
            });
            if (flag == 0 && document.getElementById('DropDownList4').value == "") {
                $("#sela").multiselect("disable");
            }
            
        });

        function showValues() {
            valuestr = $('#sela').multiselect("MyValues");
            document.getElementById('param1').value = valuestr;
        }

        function showMultiselect() {
            $("#sela").multiselect("enable");
            flag = 1;
        }

    </script>

    <style type="text/css">
    .style2
    {
        width: 10px;
    }    

    .one, .two{
	    float:left;	     
    }
    .one{
	    width:520px;
    }
    .two{
         width:500px;
    }

    </style>
</head>

<body onkeydown="if (event.keyCode == 116){window.location.reload();return false;}">
 <form id="formaspx" runat="server">
 <div id="waitImg" class="wait" style="position:absolute; top:5px; height:20px; width:100px; right:10px;display:none;"></div>
 <div id="Navi" class="Navi">
 <div class="NaviList" >
         <asp:PlaceHolder ID="plhNavi"  Runat="Server" ></asp:PlaceHolder> 
 </div>
 </div>

<div id="Main" class="MainReport">    
    <div>
    </div>
            <div>
    <div style="width:750px;float:left;" >   
      <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>
      
    <table>
    <tr style=" height:30px;">
    <td style=" width:5px;"></td>
    <td style=" width:70px;">
        <asp:Label ID="Label4" runat="server" Text="类型：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
    </td>
    <td style=" width:111px;">
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
            height="24px" ontextchanged="DropDownList1_TextChanged" Width="105px">
        </asp:DropDownList>
    </td>
    <td class="style2">&nbsp;&nbsp;</td>
                <td style=" width:70px;">
                    <asp:Label ID="Label3" runat="server" Text="品名：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
                </td>
                <td style=" width:166px;">
                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                        Height="24px" ontextchanged="DropDownList2_TextChanged" Width="160px" >
                    </asp:DropDownList>
                </td>
                <td class="style2">&nbsp;&nbsp;</td>
                <td style=" width:70px;">
                    <asp:Label ID="Label2" runat="server" Text="测试方案：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
                </td>
                <td style=" width:186px;">
                    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                    Height="24px" Width="180px" ontextchanged="DropDownList3_TextChanged">
                    </asp:DropDownList>
                </td>
                <td class="style2">&nbsp;&nbsp;</td>               
    </tr>
            <tr style=" height:30px;">
                <td style=" width:5px;"></td>
                <td style=" width:70px;">
                    <asp:Label ID="Label1" runat="server" Text="SN：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
                </td>
                <td style=" width:111px;"><asp:TextBox ID="TextBox1" runat="server" Width="100px" height="18px"></asp:TextBox>
                </td>
                <td class="style2">&nbsp;</td>
                <td style=" width:70px;">
                    <asp:Label ID="Label5" runat="server" Text="起始时间：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
                </td>
                <td style=" width:166px;"><asp:TextBox ID="TextBox5" runat="server" Width="155px"  height="18px"
                        ToolTip="2015/1/1 00:00:00"></asp:TextBox>
                </td>
                <td class="style2">&nbsp;</td>
                <td style=" width:70px;">
                    <asp:Label ID="Label6" runat="server" Text="截止时间：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
                </td>
                <td style=" width:186px;"><asp:TextBox ID="TextBox6" runat="server" Width="175px"  height="18px"
                        ToolTip="2015/1/1 23:59:59"></asp:TextBox>
                </td>
                <td class="style2">&nbsp;&nbsp;</td>          
            </tr>
        </table>

        <table>
           <tr style=" height:30px;">
                <td style=" width:5px;"></td>
                <td style=" width:70px;">
                    <asp:Label ID="Label9" runat="server" Text="分析类型：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
                </td>
                 <td style=" width:111px;">
                    <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" 
                        height="24px" Width="105px" 
                         onselectedindexchanged="DropDownList5_SelectedIndexChanged">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>失效模式图</asp:ListItem>
                        <asp:ListItem>结果分布图</asp:ListItem>
                        <asp:ListItem>箱线图</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style2">&nbsp;&nbsp;</td>
                <td style=" width:70px;">
                    <asp:Label ID="Label10" runat="server" Text="测试项：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
                </td>
                <td style=" width:166px;">
                    <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="False" 
                        Height="24px" Width="160px">
                    </asp:DropDownList>
                </td>
                <td style=" width:283px;"></td>
               
            </tr>
        </table>
         
                 </ContentTemplate>
    </asp:UpdatePanel>

    </div>

    <div>
       <table>
            <tr style=" height:30px;">
              <td style=" width:70px;">
                    <asp:Label ID="Label8" runat="server" Text="报表表头：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
                </td>
                <td style=" width:161px;">
                    <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Height="24px" Width="155px"
                    ontextchanged="DropDownList4_TextChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style=" height:30px;">
             <td style=" width:70px;">
                    <asp:Label ID="Label7" runat="server" Text="表头筛选：" style="font-family :微软雅黑; vertical-align:middle;" Font-Size="14px"></asp:Label>
                </td>
                <td style=" width:161px;">
                    <select id ="sela" name="sela" size="5"  runat="server" multiple="true" style="display:none;" > 
                    </select> 
                    <input style='display:none;' id='param1' runat="server" />
                </td>
            </tr>
       </table>

        <table>
            <tr  style=" height:30px;">
             <td style=" width:100px;">
                   <asp:Button ID="Button1" runat="server" Text="" Height="28px" Width="90px" class="btQuery"
                        OnClientClick="javascript:document.getElementById('waitImg').style.display='block';showValues();"
                        onclick="Button1_Click"/>
                </td>      
               <td style=" width:100px;">
                   <asp:Button ID="Button2" runat="server" Text="" Height="28px" Width="90px" class="btExport"
                        onclick="Button2_Click"/>
                </td>
                <td>
                   <asp:Button ID="Button3" runat="server" Text="" Height="28px" Width="90px" class="btStatistics"
                        onclick="Button3_Click"/>
                </td>
            </tr>
        </table>
    </div>

    <%--失效项数目统计柱状图--%>
    <div style=" margin-left: -35px;">
       <asp:Chart ID="Chart2" runat="server" Width="1072px" Height="510px" 
            Visible="false">
            <series><asp:Series Name="Series1"></asp:Series></series>
            <chartareas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></chartareas>
        </asp:Chart>
         
        <asp:Chart ID="Chart1" runat="server" Width="1072px" Height="510px" style="margin-top:30px;"
            Visible="false" >
            <Series>
                <asp:Series Name="Series1" XValueType="Double">
                </asp:Series>
                <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="Series2">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>         
    </div>

    <%--折线图--%>
    <div style=" margin-left: 10px;margin-top :30px;">
       <asp:Chart ID="LineChartTemp1" runat="server" Width="500px" Height="280px" 
            Visible="false" BorderlineColor="Black" BorderlineDashStyle="Solid">
            <Series>
                 <asp:Series Name="SN1"  ChartType="Line" Color="Red" BorderWidth="2" 
                     MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False"></asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN2" Color="Orange" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN3" 
                     Color="GreenYellow" BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" 
                     Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN4" Color="Green" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN5" Color="#3399ff" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN6" Color="Blue" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN7" Color="Purple" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN8" 
                     Color="Chocolate" BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" 
                     Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN9" Color="#ff66ff" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN10" Color="Gray" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
            </Series>
            <chartareas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></chartareas>
            <Legends>
                <asp:Legend Name="Legend1">
                </asp:Legend>
            </Legends>
        </asp:Chart>

        <asp:Chart ID="LineChartTemp2" runat="server" Width="500px" Height="280px" 
            Visible="false" BorderlineColor="Black" BorderlineDashStyle="Solid">
            <Series>
                 <asp:Series Name="SN1"  ChartType="Line" Color="Red" BorderWidth="2" 
                     MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False"></asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN2" Color="Orange" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN3" 
                     Color="GreenYellow" BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" 
                     Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN4" Color="Green"  
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN5" Color="#3399ff" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN6" Color="Blue" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN7" Color="Purple" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN8" 
                     Color="Chocolate" BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" 
                     Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN9" Color="#ff66ff" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN10" Color="Gray" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
            </Series>
            <chartareas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></chartareas>
            <Legends>
                <asp:Legend Name="Legend1">
                </asp:Legend>
            </Legends>
        </asp:Chart>
     
        <asp:Chart ID="LineChartTemp3" runat="server" Width="500px" Height="280px" 
            Visible="false" BorderlineColor="Black" BorderlineDashStyle="Solid">
            <Series>
                 <asp:Series Name="SN1"  ChartType="Line" Color="Red" BorderWidth="2" 
                     MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False"></asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN2" Color="Orange" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN3" 
                     Color="GreenYellow" BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" 
                     Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN4" Color="Green"  
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN5" Color="#3399ff" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN6" Color="Blue" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN7" Color="Purple" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN8" 
                     Color="Chocolate" BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" 
                     Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN9" Color="#ff66ff" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
                 <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="SN10" Color="Gray" 
                     BorderWidth="2" MarkerStyle="Circle" MarkerSize="6" Legend="Legend1" IsVisibleInLegend="False">
                </asp:Series>
            </Series>
            <chartareas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></chartareas>
            <Legends>
                <asp:Legend Name="Legend1">
                </asp:Legend>
            </Legends>
        </asp:Chart>
    </div>

    <%--箱线图--%>
    <div style=" margin-left: 10px;margin-top :30px;">
       <asp:Chart ID="BoxChartTemp1" runat="server" Width="500px" Height="320px" 
            Visible="false" BorderlineColor="Black" BorderlineDashStyle="Solid">
            <Series>                
                <asp:Series ChartType="BoxPlot" Name="BoxSeries1">
                </asp:Series>
                <asp:Series  ChartArea="ChartArea1" Name="PointSeries1" XValueType="Double">
                </asp:Series>
            </Series>
            <chartareas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></chartareas>
        </asp:Chart>

        <asp:Chart ID="BoxChartTemp2" runat="server" Width="500px" Height="320px" 
            Visible="false" BorderlineColor="Black" BorderlineDashStyle="Solid">
            <Series>              
                <asp:Series ChartType="BoxPlot" Name="BoxSeries2">
                </asp:Series>
                <asp:Series ChartArea="ChartArea1" Name="PointSeries2" XValueType="Double">
                </asp:Series>
            </Series>
            <chartareas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></chartareas>
        </asp:Chart>
     
        <asp:Chart ID="BoxChartTemp3" runat="server" Width="500px" Height="320px" 
            Visible="false" BorderlineColor="Black" BorderlineDashStyle="Solid">
            <Series>                
                <asp:Series ChartType="BoxPlot" Name="BoxSeries3">
                </asp:Series>
                <asp:Series ChartArea="ChartArea1" Name="PointSeries3" XValueType="Double">
                </asp:Series>
            </Series>
            <chartareas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></chartareas>
        </asp:Chart>

        <asp:Chart ID="BoxChartTempAll" runat="server" Width="500px" Height="320px" 
            Visible="false" BorderlineColor="Black" BorderlineDashStyle="Solid">
            <Series>
                <asp:Series ChartType="BoxPlot" Name="BoxSeries4">
                </asp:Series>
                <asp:Series ChartArea="ChartArea1" Name="PointSeries4" XValueType="Double">
                </asp:Series>
            </Series>
            <chartareas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></chartareas>
        </asp:Chart>

    </div>

     <div>
    <table>
                <tr>
                    <td>&nbsp;</td>
                    <td>
     <asp:GridView ID="GridView1" class="Grid" runat="server" Width="238px" CellPadding="4" 
                            EnableViewState="False">
                <HeaderStyle CssClass="tHStyleCenterBottom" Wrap="false"/>
                 <RowStyle Height="29px"/>
            </asp:GridView>
            </td>
                </tr>
            </table>
    </div>

    <%--测试项正态分布统计图--%>
     <div style=" margin-top :30px;" class="one">
     <asp:Image ID="Image1" runat="server" Visible="false"/>
     </div>
     <div style=" margin-top :60px;" class="two">  
      <table id="CPKtable" runat="server" style=" border:none; border-collapse:collapse;" >
      </table>
     </div>

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

   </script> 
 </form>
</body>
</html>

