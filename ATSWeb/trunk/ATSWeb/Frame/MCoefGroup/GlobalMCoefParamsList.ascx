<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalMCoefParamsList.ascx.cs" Inherits="Frame_MCoefGroup_GlobalMCoefParamsList" %>

<div>
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;table-layout:fixed; width:975px;">
     <tr>
    <td width="25px"></td>        
    <td width="150px"></td>        
    <td width="200px"></td>
    <td width="100px"></td>        
    <td width="100px"></td>
    <td width="100px"></td>        
    <td width="100px"></td>
    <td width="100px"></td>   
    <td width="100px"></td>     
    </tr>
 
 <tr>
        <td ID="TH0Title" class="tHStyleCenter" width="25px"  runat="server">
        </td>
        <td ID="TH1Title" class="tHStyleCenter" width="150px" runat="server">
         <asp:Label ID="TH1" runat="server" Text="类型"></asp:Label>
        </td>
        <td ID="TH2Title" class="tHStyleCenter" width="200px" runat="server">
        <asp:Label ID="TH2" runat="server" Text="名称"></asp:Label>
        </td>
        <td ID="TH3Title" class="tHStyleCenter" width="100px" runat="server">
        <asp:Label ID="TH3" runat="server" Text="通道"></asp:Label>
        </td>
        <td ID="TH4Title" class="tHStyleCenter" width="100px" runat="server">
        <asp:Label ID="TH4" runat="server" Text="页数"></asp:Label>
        </td>
         <td ID="TH5Title" class="tHStyleCenter" width="100px" runat="server">
        <asp:Label ID="TH5" runat="server" Text="开始地址"></asp:Label>
        </td>
        <td ID="TH6Title" class="tHStyleCenter" width="100px" runat="server">
        <asp:Label ID="TH6" runat="server" Text="长度"></asp:Label>
        </td>
        <td ID="TH7Title" class="tHStyleCenter" width="100px" runat="server">
        <asp:Label ID="TH7" runat="server" Text="格式"></asp:Label>
        </td>
        <td ID="TH8Title" class="tHStyleCenter" width="100px" runat="server">
        <asp:Label ID="TH8" runat="server" Text="放大倍数"></asp:Label>
        </td>
        </tr>
<tr id="ContentTR" runat="server" style=" background-color:White;">
<td width="25px" class="tdCenter">
    <asp:CheckBox ID="chkIDMCoefParam" runat="server" />
    </td>
    <td width="150px" class="tdLeft">
    <asp:Label ID="txtItemType" runat="server"  Enabled="False" >ItemType</asp:Label>    
    </td>
    <td width="200px" class="tdLeft">
    <asp:LinkButton ID="lnkItemName"
        runat="server">ItemName</asp:LinkButton>     
        </td>
    <td width="100px" class="tdCenter">
    <asp:Label ID="txtChannel" runat="server" Enabled="False" >Channel</asp:Label>
    </td>
    <td width="100px" class="tdCenter">         
    <asp:Label ID="txtPage" runat="server"  >Page</asp:Label>       
    </td>
    <td width="100px" class="tdCenter">    
    <asp:Label ID="txtStartAddress" runat="server"></asp:Label> 
    </td>
    <td width="100px" class="tdCenter">    
    <asp:Label ID="txtLength" runat="server"></asp:Label>
    </td>    
    <td width="100px" class="tdCenter">
        <asp:Label ID="txtFormat" runat="server"  ></asp:Label>
    </td>
    <td width="100px" class="tdCenter">
        <asp:Label ID="txtAmplify" runat="server"  ></asp:Label>
    </td>
</tr>
</table>
</div>
    
    
