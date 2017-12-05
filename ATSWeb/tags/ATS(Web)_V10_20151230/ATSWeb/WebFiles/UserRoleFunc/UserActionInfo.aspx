<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="UserActionInfo.aspx.cs" Inherits="WebFiles_UserRoleFunc_UserActionInfo" %>

<%@ Register src="../../Frame/OptionButtons.ascx" tagname="OptionButtons" tagprefix="uc1" %>
<%@ Register src="../../Frame/UserRoleFunc/PNActionList.ascx" tagname="PNActionList" tagprefix="uc2" %>
<%--<%@ Register src="../../Frame/UserRoleFunc/PlanActionList.ascx" tagname="PlanActionList" tagprefix="uc3" %>--%>

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

        <style type="text/css">
        .style1
        {
            height: 20px;
        }
    
        .style2
        {
            height: 15px;
        }
        
        .style3
        {
            width: 52px;
        }
        </style>

        <div>    
        <table>
            <tr> <td height="2px"></td></tr>
            <tr>
               <td> 
                   <asp:Label ID="Label1" runat="server" Text="[ProductionType]" Font-Bold="True"></asp:Label>
                </td>
            </tr>

            <tr>
               <td class="style1"> 
                   <asp:DropDownList ID="DropDownListType" runat="server" Height="20px" 
                       Width="144px"  AutoPostBack="True" 
                       onselectedindexchanged="DropDownListType_SelectedIndexChanged">
                   </asp:DropDownList>
                </td>
            </tr>

            <tr>
               <td class="style2" > </td>
            </tr>

            <tr>
               <td class="style1"> 
                   <asp:Label ID="Label2"  Width="144px"  runat="server" Text="[PN权限]" Font-Bold="True"></asp:Label>
                </td>
            </tr>

        </table>

        <table  style="margin-left: 450px; margin-top: -24px;" >
           <tr>
             <td>
                <asp:Label ID="Label3" runat="server" Text="[TestPlan权限]" Font-Bold="True"></asp:Label>
             </td>
           </tr>
        </table>

        
        <table>
          <tr>   
            <td width="390px"   style="vertical-align:top "> <asp:PlaceHolder ID="plhMain" runat="server"></asp:PlaceHolder></td>           
            <td class="style3"></td>       
            <td style="vertical-align:top">
             <asp:GridView ID="GridView1" class="GridAction" runat="server" Width="500px"
                        onrowcreated="GridView1_RowCreated" 
                        AutoGenerateColumns="False" >
            
            <Columns>
            <%-- <asp:BoundField DataField="PNID" HeaderText="PNID"/>
            <asp:BoundField DataField="PlanID" HeaderText="PlanID"  /> --%>
                             
            <asp:TemplateField HeaderText="PNID" Visible="False">
            <ItemTemplate>
                <asp:Label ID="PNIDLabel" runat="server" Text="Label"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>    
                 
            <asp:TemplateField HeaderText="PlanID" Visible="False">
            <ItemTemplate>
                <asp:Label ID="PlanIDLabel" runat="server" Text="Label"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField> 
              
             <asp:BoundField DataField="TestPlan" HeaderText="TestPlan" />   
                     
            <asp:TemplateField HeaderText="Edit?">
            <ItemTemplate>
               <asp:CheckBox ID="CheckBoxEdit" runat="server" Enabled="False" /> 
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Delete?" >
            <ItemTemplate>
               <asp:CheckBox ID="CheckBoxDelete" runat="server" Enabled="False" /> 
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Run?" >
            <ItemTemplate>
               <asp:CheckBox ID="CheckBoxRun" runat="server" Enabled="False" /> 
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="ActionID" Visible="False">
            <ItemTemplate>
                <asp:Label ID="ActionIDLabel" runat="server" Text="Label"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField> 

            </Columns>
                 <EmptyDataTemplate >
                       <%--<table>--%>
                       <%-- <tr>--%>   
                           <th width="260px"  height="23px" ID="TH4Title" class="tHStyleCenter" runat="server">
                               <asp:Label ID="TH4" class="tdLeft" runat="server" Text="TestPlan"></asp:Label>
                           </th>
                           <th width="80px" height="23px" ID="TH1Title" class="tHStyleCenter" runat="server">
                               <asp:Label ID="TH1" class="tdLeft" runat="server" Text="Edit?"></asp:Label>
                           </th>
                           <th width="80px" height="23px" ID="TH2Title" class="tHStyleCenter" runat="server">
                               <asp:Label ID="TH2" class="tdLeft" runat="server" Text="Delete?"></asp:Label>
                           </th>
                            <th width="80px" height="23px" ID="TH3Title" class="tHStyleCenter" runat="server">
                               <asp:Label ID="TH3" class="tdLeft" runat="server" Text="Run?"></asp:Label>
                           </th>

                       <%-- </tr>  --%>
                       <%-- </table>--%>
                 </EmptyDataTemplate>
            <HeaderStyle BackColor="#87CEFA" Font-Bold="True" ForeColor="Black" CssClass="tHStyleCenter" height="23px" Wrap="false"/>
        </asp:GridView></td>
          </tr>
        </table>
            
           

        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="OptionContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
        <ContentTemplate>
    <table border="0" cellspacing="0" width="100%" height="100%" align="center" valign="middle">
    	<tr valign="middle">
    		<td id="OptionButtonContent" align="center" valign="middle">                            
                <uc1:OptionButtons ID="OptionButtons1" runat="server" />                           
            </td>
    	</tr>
    </table>  
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>