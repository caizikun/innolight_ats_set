<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="TestplanSelfInfor.aspx.cs" Inherits="ASPXTestplanSelfInfor" %>

<%@ Register src="../../Frame/TestPlan/TestPlanInfor.ascx" tagname="TestPlanInfor" tagprefix="uc1" %>

<%@ Register src="../../Frame/OptionButtons.ascx" tagname="OptionButtons" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ModulesNameListContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviContent" Runat="Server">
 <table border="0" cellspacing="0" cellpadding="0"  class="NaviLeftMargin">
	<tr class="tdCenter" >
		<td id="tdVCenter">
         <asp:PlaceHolder ID="plhNavi"  Runat="Server" ></asp:PlaceHolder> 
        </td>
	</tr>
</table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="UserInforContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
   </asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                <ContentTemplate>

                <div>
       <table border="0" cellspacing="0" cellpadding="0" width="100%">
    	<tr>
    		<td id="TestPlanSelfInforContent">
             <asp:PlaceHolder ID="TestPlanSelfInfor" Runat="Server" ></asp:PlaceHolder> 
            </td>
    	</tr>
    </table>
    </div>

               </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="OptionContent" Runat="Server">
 <table border="0" cellspacing="0" width="100%" height="100%" align="center" valign="middle">
    	<tr valign="middle">
    		<td id="OptionButtonContent" align="center" valign="middle">                            
                <uc2:OptionButtons ID="OptionButtons1" runat="server" />                           
            </td>
    	</tr>
    </table>   
</asp:Content>

