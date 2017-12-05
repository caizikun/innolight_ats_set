<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalModelList.ascx.cs" Inherits="Frame_APPModel_GlobalModelList" %>
<div>
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;table-layout:fixed; width:855px;">
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="150px"></td>
    <td width="200px"></td>        
    <td width="90px"></td>
    <td width="90px"></td>
    <td width="100px"></td>
    </tr>

<tr>
   <td  ID="TH0Title" class="tHStyleCenter" width="25px" runat="server"></td>
   <td  ID="TH1Title" class="tHStyleCenter" width="200px" runat="server">
       <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
   </td>    
   <td  ID="TH2Title" class="tHStyleCenter" width="150px" runat="server" >
       <asp:Label ID="TH2" runat="server" Text="应用类型"></asp:Label>
   </td>
    
   <td  ID="TH3Title" class="tHStyleCenter" width="200px"  runat="server" >
       <asp:Label ID="TH3" runat="server" Text="描述"></asp:Label>
   </td>
   <td ID="TH4Title" class="tHStyleCenter" width="90px" runat="server">
       <asp:Label ID="TH4" runat="server" Text="参数"></asp:Label>
   </td>
   <td ID="TH6Title" class="tHStyleCenter" width="90px" runat="server">
       <asp:Label ID="TH6" runat="server" Text="权重"></asp:Label>
   </td>
   <td ID="TH5Title" class="tHStyleCenter" width="100px" runat="server">
       <asp:Label ID="TH5" runat="server" Text="模型关系"></asp:Label>
   </td>
</tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
    <td width="25px" class="tdCenter">
            <asp:CheckBox ID="chkIDModel" runat="server" />    
        </td>

        <td width="200px" class="tdLeft">
            <asp:LinkButton ID="lnkItemName"
                runat="server">ShowName</asp:LinkButton>
            <asp:TextBox ID="txtItemName" runat="server" Visible="false"></asp:TextBox>
        </td>

       

        <td width="150px" class="tdLeft">
            <asp:Label ID="ddlAppName" runat="server"></asp:Label>
            <%-- 
            <asp:DropDownList ID="ddlAppName"
                runat="server">
            </asp:DropDownList>
            --%>
        </td>
        <td width="200px" class="tdLeft">
            <asp:TextBox ID="txtItemDescription" runat="server"></asp:TextBox>
            <asp:Label ID="lblItemDescription" runat="server"></asp:Label>
        </td>
        <td width="90px" class="tdCenter">
            <asp:LinkButton ID="lnkViewParams"
                runat="server"> 查看 </asp:LinkButton>
        </td>
         <td width="90px" class="tdCenter">
            <asp:Label ID="lbItemWeight" runat="server"></asp:Label>
        </td>
        <td width="100px" class="tdCenter">
            <asp:LinkButton ID="lnkViewRelation"
                runat="server"> 查看 </asp:LinkButton>
        </td>
</tr>
</table>
</div>
        