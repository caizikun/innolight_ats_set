<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalEquipList.ascx.cs" Inherits="Frame_Equipment_GlobalEquipList" %>
      <div>
      <table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;table-layout:fixed; width:665px;">
       <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="150px"></td>
    <td width="200px"></td>        
    <td width="90px"></td>
    </tr>
      <tr>
      <td ID="TH0Title" class="tHStyleCenter" width="25px" runat="server">
      </td>
      <td ID="TH1Title" class="tHStyleCenter" width="200px" runat="server">
      <asp:Label ID="TH1" runat="server" Text="别名"></asp:Label>
      </td>
       
      <td ID="TH2Title" class="tHStyleCenter" width="150px" runat="server">
      <asp:Label ID="TH2" runat="server" Text="设备类型"></asp:Label>
      </td>
      <td ID="TH3Title" class="tHStyleCenter" width="200px" runat="server">
      <asp:Label ID="TH3" runat="server" Text="描述"></asp:Label>
      </td>
      <td ID="TH4Title" class="tHStyleCenter" width="90px" runat="server">
      <asp:Label ID="TH4" runat="server" Text="设备参数"></asp:Label>
      </td>
      
      </tr>
        <tr id="ContentTR" runat="server" style=" background-color:White;">
         <td width="25px" class="tdCenter">
            <asp:CheckBox ID="chkIDEquip" runat="server" />    
        </td>
        <td width="200px" class="tdLeft">
            <asp:LinkButton ID="lnkItemName"
                runat="server">ShowName</asp:LinkButton>
        </td>
     
        <td width="150px" class="tdLeft">
            <asp:Label ID="lblItemType" runat="server"></asp:Label>
        </td>
        <td width="200px" class="tdLeft">
            <asp:Label ID="lblItemDescription" runat="server"></asp:Label>
        </td>
        <td width="90px" class="tdCenter">
            <asp:LinkButton ID="lnkViewParams"
                runat="server"> 查看 </asp:LinkButton>
        </td>
        </tr>
        </table>
      </div>
        
       