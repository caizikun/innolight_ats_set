<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AscxDDLTxt.ascx.cs" Inherits="Frame_AscxDDLTxt" %>
<span style="width: 0px;position:relative;"  id="ddlTxt" runat="server" >
        <asp:DropDownList ID="ddlItem" runat="server" AutoPostBack ="true"
    Width="160px" onselectedindexchanged="ddlItem_SelectedIndexChanged" 
    Height="33px" style="margin-bottom: 0px">            
        </asp:DropDownList>
    
    <asp:TextBox ID="txtItem" runat="server" width="145px" style="position:absolute; margin-left:-160px;"
        ontextchanged="txtItem_TextChanged" AutoPostBack ="true"></asp:TextBox>

 </span> 