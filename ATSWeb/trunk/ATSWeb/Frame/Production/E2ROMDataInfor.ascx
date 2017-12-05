<%@ Control Language="C#" AutoEventWireup="true" CodeFile="E2ROMDataInfor.ascx.cs" Inherits="ASCXE2ROMDataInfor" %>
<div>
    <table  cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-break:break-word;word-break : break-all;"> 
    <tr>
    <td width="100px"></td>        
    <td width="100px"></td>        
    <td width="100px"></td>
    <td width="280px"></td>
    <td width="410px"></td> 
    </tr>

    <tr>  
    <td width="100px" ID="TH1Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH1"  runat="server" Text="地址(Hex)"></asp:Label>
    </td>
    <td width="100px" ID="TH2Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH2"  runat="server" Text="地址(Dec)"></asp:Label>
    </td>
     <td width="100px" ID="TH3Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH3"   runat="server" Text="内容(Hex)"></asp:Label>
    </td>
     <td width="280px" ID="TH4Title" class="tHStyleCenter" runat="server" >
        <asp:Label ID="TH4"  runat="server" Text="字段名"></asp:Label>
    </td>
    <td width="410px" ID="TH5Title" class="tHStyleCenter" runat="server">
        <asp:Label ID="TH5"  runat="server" Text="字段描述"></asp:Label>
    </td>
   
    </tr>    
    <tr id="ContentTR" runat="server" style=" background-color:White;">         
    <td class="tdCenter1" width="100px">
     <asp:Label ID="AddressHexText" runat="server"></asp:Label>
    </td>    
    <td class="tdCenter1" width="100px">
    <asp:Label ID="AddressDecText"   runat="server"></asp:Label>
    </td>
    <td class="tdCenter1" width="100px">
    <asp:TextBox ID="ContentText" width="90px" height="22px" AutoPostBack="true" runat="server"            
        ontextchanged="ContentText_TextChanged"></asp:TextBox>
    </td>
   
    <td class="tdLeft1" width="280px">    
   <asp:Label ID="FiledNameText"   Text="" runat="server"></asp:Label>   
    </td>  
      
    <td class="tdLeft1" width="410px">   
     <asp:Label ID="FiledDescriptionText"   Text="" runat="server"></asp:Label>     
    </td>
   <td> 
       <asp:RegularExpressionValidator ID="REVContentText" runat="server" 
    ControlToValidate="ContentText" ErrorMessage="请输入两个十六进制数" 
    ValidationExpression="" ForeColor="Red" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
   </td>
    </tr>
    </table>    
  
</div>

