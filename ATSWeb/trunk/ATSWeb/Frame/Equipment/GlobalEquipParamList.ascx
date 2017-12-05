<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalEquipParamList.ascx.cs" Inherits="Frame_Equipment_GlobalEquipParamList" %>
  <div>
  <table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;table-layout:fixed; width:925px;">
     <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="200px"></td>
    <td width="100px"></td>        
    <td width="150px"></td>
    <td width="250px"></td>
    </tr>
  <tr>
  <td ID="TH0Title" class="tHStyleCenter" width="25px" runat="server">
  </td>
  <td  ID="TH1Title" class="tHStyleCenter"  width="200px" runat="server">
      <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
  </td>
  <td  ID="TH5Title" class="tHStyleCenter"  width="200px" runat="server">
      <asp:Label ID="TH5" runat="server" Text="别名"></asp:Label>
  </td>
  <td ID="TH3Title" class="tHStyleCenter" width="100px" runat="server">
  <asp:Label ID="TH3" runat="server" Text="参数类型"></asp:Label>
  </td>
   <td  ID="TH2Title" class="tHStyleCenter" width="150px" runat="server">
   <asp:Label ID="TH2" runat="server" Text="值"></asp:Label>
  </td>
   <td  ID="TH4Title" class="tHStyleCenter" width="250px" runat="server">
   <asp:Label ID="TH4" runat="server" Text="描述"></asp:Label>
  </td>  
  </tr>
  <tr  id="ContentTR" runat="server" style=" background-color:White;">
    <td width="25px"  class="tdCenter">
    <asp:CheckBox ID="chkIDEquipParam" runat="server" />
    </td> 
    <td  width="200px" class="tdLeft">
    <asp:LinkButton ID="lnkItem" runat="server"></asp:LinkButton>
    </td> 
    <td width="200px" class="tdLeft">
    <asp:Label ID="lblShowName" runat="server"></asp:Label>
    </td> 
    <td width="100px" class="tdLeft">
    <asp:Label ID="lblItemType" runat="server"></asp:Label>    
    </td> 
    <td width="150px" class="tdLeft">
    <asp:Label ID="lblItemValue" runat="server"></asp:Label>
    </td> 
    <td width="250px" class="tdLeft">
    <asp:Label ID="lblItemDescription" runat="server"></asp:Label>    
    </td>
  </tr>
  </table>
 </div>
  