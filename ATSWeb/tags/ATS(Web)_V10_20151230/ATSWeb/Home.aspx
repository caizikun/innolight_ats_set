<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Home.aspx.cs" Inherits="_Home" %><%@ Register src="Frame/LinkBtn.ascx" tagname="LinkBtn" tagprefix="uc1" %>
<%@ Register src="~/Frame/imgBtn.ascx" tagname="imgBtn" tagprefix="ucImgBtn" %>
<%@ Register src="~/Frame/AccountInfo.ascx" tagname="AccountInfo" tagprefix="ucAccountInfo" %>


<%@ Register src="Frame/OptionButtons.ascx" tagname="OptionButtons" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ModulesNameListContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="UserInforContent" Runat="Server">   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">  
<%-- <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"> <html xmlns="http://www.w3.org/1999/xhtml"> <head> <meta http-equiv="Content-Type" content="text/html; charset=gb2312" /> <title></title> </head>--%>
     
      <table  width="100%" border="0">
    	<tr valign="middle" >
    		<td  align="center" valign="middle">
     <div align="center"  id="colee" style="overflow:hidden;height:600px; width:600px;"> 
     <div align="center"  id="colee1" > 
     <p><img src="Images/1home.jpg"></p> 
     <p><img src="Images/2home.jpg"></p>
      <p><img src="Images/3home.jpg"></p> 
      <p><img src="Images/4home.jpg"></p> 
      <p><img src="Images/5home.jpg"></p>
       <p><img src="Images/6home.jpg"></p> 
       </div> 
       <div align="center" valign="middle" id="colee2"></div> 
       </div>        
            </td>
    	</tr>
    </table>
     
    
   
       <script type="text/javascript">
           var speed = 20;
           var colee2 = document.getElementById("colee2");
           var colee1 = document.getElementById("colee1");
           var colee = document.getElementById("colee");
           colee2.innerHTML = colee1.innerHTML; //��¡colee1Ϊcolee2 
           function Marquee1() {
               //��������colee1��colee2����ʱ 
               if (colee2.offsetTop - colee.scrollTop <= 0) { colee.scrollTop -= colee1.offsetHeight; }
               else { colee.scrollTop++ }
           }
           var MyMar1 = setInterval(Marquee1, speed); //���ö�ʱ�� //�������ʱ�����ʱ���ﵽ����ֹͣ��Ŀ��
           colee.onmouseover = function () { clearInterval(MyMar1) } //����ƿ�ʱ���趨ʱ�� 
           colee.onmouseout = function () { MyMar1 = setInterval(Marquee1, speed) }
       </script> 
      
 </asp:Content>
