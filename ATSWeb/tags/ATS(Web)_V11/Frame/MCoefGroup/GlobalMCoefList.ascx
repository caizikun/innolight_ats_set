﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalMCoefList.ascx.cs" Inherits="Frame_MCoefGroup_GlobalMCoefList" %>
        <div>
        <table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
        <tr>
        <th ID="TH0Title" class="tHStyleLeft" width="5px" runat="server">
        </th>
        <th ID="TH1Title" class="tHStyleLeft" width="200px" runat="server">
         <asp:Label ID="TH1" runat="server" Text="Item"></asp:Label>
        </th>
        <th ID="TH2Title" class="tHStyleLeft"  width="150px" runat="server">
        <asp:Label ID="TH2" runat="server" Text="TypeName"></asp:Label>
        </th>
        <th ID="TH3Title" class="tHStyleLeft" width="200px" runat="server">
        <asp:Label ID="TH3" runat="server" Text="Description"></asp:Label>
        </th>
        <th ID="TH4Title" class="tHStyleLeft" width="150px" runat="server">
        <asp:Label ID="TH4" runat="server" Text="Params"></asp:Label>
        </th>
        
        </tr>
       <tr id="ContentTR" runat="server">
       <td width="5px" class="tdLeft" style="border:solid #000 1px">
            <asp:CheckBox ID="chkIDMCoef" runat="server" />    
        </td>
        <td  width="200px" class="tdLeft" style="border:solid #000 1px">
            <asp:LinkButton ID="lnkItemName" runat="server">ItemName</asp:LinkButton>
        </td>
        <td  width="150px" class="tdLeft" style="border:solid #000 1px">         
              <asp:Label ID="ddlTypeName" runat="server"></asp:Label>
        </td>

        <td width="200px" class="tdLeft" style="border:solid #000 1px">
             <asp:Label ID="txtItemDescription" runat="server"></asp:Label>
            </td>

        <td width="150px" class="tdLeft" style="border:solid #000 1px">
            <asp:LinkButton ID="lnkViewParams"
                runat="server">View </asp:LinkButton>
        </td>
        	</tr>
        </table>
        </div>
        