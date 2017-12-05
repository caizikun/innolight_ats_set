<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ModuleType.aspx.cs" Inherits="ASPXTestPlanModuleType" %>
<%@ Register src="../../Frame/TestPlan/TestplanType.ascx" tagname="ModuleType" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"  Runat="Server" >
    
    <table border="0" cellspacing="0">
    
    	<tr >
    		<td id="ModuleTypeContent" >
             <asp:PlaceHolder ID="ModuleSelfInfor" Runat="Server" ></asp:PlaceHolder> 
            </td>
    	</tr>
    </table>    
  
</asp:Content>
<asp:Content  ID="Content3" ContentPlaceHolderID="NaviContent" Runat="Server">
<table border="0" cellspacing="0" cellpadding="0"  class="NaviLeftMargin" >
	<tr>
		<td id="tdVCenter">
         <asp:PlaceHolder ID="plhNavi"  Runat="Server" ></asp:PlaceHolder> 
        </td>
	</tr>
</table>
</asp:Content>
