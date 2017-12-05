<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalSpecsList.ascx.cs" Inherits="Frame_GlobalSpecs_GlobalSpecsList" %>
      <div>
      <table cellspacing="0" cellpadding="0" style="border:none; border-collapse:collapse;table-layout:fixed; width:505px;">
    <tr>
    <td width="25px"></td>        
    <td width="200px"></td>        
    <td width="80px"></td>
    <td width="200px"></td>        
    </tr>

      <tr>
      <td ID="TH0Title" class="tHStyleCenter" width="25px" runat="server">
      </td>
      <td ID="TH1Title" class="tHStyleCenter" width="200px" runat="server">
      <asp:Label ID="TH1" runat="server" Text="名称"></asp:Label>
      </td>
          
      <td ID="TH2Title" class="tHStyleCenter"  width="80px" runat="server">
      <asp:Label ID="TH2" runat="server" Text="单位"></asp:Label>
      </td>
      <td ID="TH3Title" class="tHStyleCenter" width="200px" runat="server">
      <asp:Label ID="TH3" runat="server" Text="描述"></asp:Label>
      </td>
      </tr>
        <tr id="ContentTR" runat="server" style=" background-color:White;">
         <td width="25px" class="tdCenter">
            <asp:CheckBox ID="chkID" runat="server"/>    
        </td>
        <td width="200px" class="tdLeft">
            <asp:LinkButton ID="lnkItemName"
                runat="server">ItemName</asp:LinkButton>
        </td>
        <td width="80px" class="tdLeft">
            <asp:Label ID="lblUnit" runat="server"></asp:Label>
        </td>
        <td width="200px" class="tdLeft">
            <asp:Label ID="lblItemDescription" runat="server"></asp:Label>
        </td>
        </tr>
        </table>
      </div>
        
       