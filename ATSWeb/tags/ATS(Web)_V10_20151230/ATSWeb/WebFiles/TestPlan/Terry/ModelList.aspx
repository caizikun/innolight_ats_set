<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true"  EnableEventValidation="false" CodeFile="ModelList.aspx.cs" Inherits="WebFiles_TestPlan_Terry_ModelList" %>

<%@ Register src="~/Frame/LinkBtn.ascx" tagname="Frame_LinkBtn" tagprefix="uc1" %>
<%@ Register src="~/Frame/TestPlan/UPDownButton.ascx" tagname="UPDownButton" tagprefix="uc2" %>
<%@ Register src="~/Frame/OptionButtons.ascx" tagname="OptionButtons" tagprefix="uc3" %>

<%@ Register src="../../../Frame/TestPlan/TestModelList.ascx" tagname="TestModelList" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ModulesNameListContent" Runat="Server">
</asp:Content>
<asp:Content  ID="Content3" ContentPlaceHolderID="NaviContent" Runat="Server">
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
        <table id="tableFC" runat="server">        
    	    <tr>
    		    <td id="EqContent" >            
                 <asp:PlaceHolder ID="plhMain" Runat="Server" ></asp:PlaceHolder> 
                </td>
    	    </tr> 
            <tr id="trFC"  visible="false" runat="server">
            <td ></td>
            </tr>
        </table>
        <table margin="10px">
            <tr >
                <td>
                    <asp:Button ID="SelectAll" runat="server" Text="SelectAll" margin-left="5px"
                        onclick="SelectAll_Click" Height="20px" Width="82px" />
                </td>
                <td>
                <asp:Button ID="DeSelectAll" runat="server" Text="DeSelectAll" margin-left="5px"
                        onclick="DeselectAll_Click" Height="20px" Width="82px" />
                </td>
            </tr>
        </table>
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="OptionContent" Runat="Server">
 <table border="0" cellspacing="0" width="100%" height="100%" align="center" valign="middle">
    	<tr valign="middle">
    		<td id="OptionButtonContent" align="center" valign="middle">                            
                <uc3:OptionButtons ID="OptionButtons1" runat="server" />                           
            </td>
    	</tr>
    </table>   
</asp:Content>
