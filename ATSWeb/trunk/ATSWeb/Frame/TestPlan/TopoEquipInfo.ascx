<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopoEquipInfo.ascx.cs" Inherits="Frame_TestPlan_TopoEquipInfo" %>
      
<table cellspacing="1" cellpadding="0">
        <tr>
        <td width="150px" class="tdStyleBorderBK">名称</td>
        <td >
         <asp:label ID="txtItemName" width="200px" height="26px" runat="server" 
                style="border:solid #CCCCCC 1px;line-height :31px;padding-left:5px;"></asp:label>
        </td>        
       </tr>

       <tr>
        <td width="150px" class="tdStyleBorderBK">排序</td>
        <td>
         <asp:label ID="lblSeq"  width="200px" height="26px" runat="server" style="border:solid #CCCCCC 1px;line-height :31px;padding-left:5px;"></asp:label>
        </td>        
       </tr>

       <tr>
       <td width="150px" class="tdStyleBorderBK">类型</td>
        <td>
         <asp:label ID="txtItemType"  width="200px" height="26px" runat="server" style="border:solid #CCCCCC 1px;line-height :31px;padding-left:5px;"></asp:label>
        </td>        
        </tr>        
       <tr>
        <td width="150px" class="tdStyleBorderBK">功能类型</td>
        <td>
            <asp:DropDownList ID="ddlRole"  width="207px" height="28px" runat="server">
                <asp:ListItem>NA</asp:ListItem>
                <asp:ListItem>TX</asp:ListItem>
                <asp:ListItem>RX</asp:ListItem>
            </asp:DropDownList>
        </td>        
       </tr>

       <tr>
       <td width="150px" class="tdStyleBorderBK">
         描述
        </td>
        <td>
            <asp:label ID="txtItemDescription"  width="200px" height="26px" runat="server" style="border:solid #CCCCCC 1px;line-height :31px;padding-left:5px;"></asp:label>            
        </td>
        </tr>
       
</table>