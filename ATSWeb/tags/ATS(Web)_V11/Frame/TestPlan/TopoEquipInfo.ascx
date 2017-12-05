<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopoEquipInfo.ascx.cs" Inherits="Frame_TestPlan_TopoEquipInfo" %>
      
<table style=" border:solid #000 1px">
        <tr>
        <td width="150px" class="tdStyleBorderBK">Item</td>
        <td >
         <asp:label ID="txtItemName" width="200px" runat="server"></asp:label>
        </td>        
       </tr>

       <tr>
        <td width="150px" class="tdStyleBorderBK">Seq</td>
        <td>
         <asp:label ID="lblSeq"  width="200px" runat="server"></asp:label>
        </td>        
       </tr>

       <tr>
       <td width="150px" class="tdStyleBorderBK">Type</td>
        <td>
         <asp:label ID="txtItemType"  width="200px" runat="server"></asp:label>
        </td>        
        </tr>        
       <tr>
        <td width="150px" class="tdStyleBorderBK">Function</td>
        <td>
            <asp:DropDownList ID="ddlRole"  width="205px" runat="server">
                <asp:ListItem>NA</asp:ListItem>
                <asp:ListItem>TX</asp:ListItem>
                <asp:ListItem>RX</asp:ListItem>
            </asp:DropDownList>
        </td>        
       </tr>

       <tr>
       <td width="150px" class="tdStyleBorderBK">
         Description
        </td>
        <td>
            <asp:label ID="txtItemDescription"  width="200px" runat="server"></asp:label>            
        </td>
        </tr>
       
</table>