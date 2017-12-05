<%@ Control Language="C#" AutoEventWireup="true" CodeFile="E2ROMDataInfor.ascx.cs" Inherits="ASCXE2ROMDataInfor" %>
<div>
    <table  cellspacing="0" cellpadding="0" style=" border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;"> 
    <tr>
   
    <th width="100px" ID="TH1Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH1"  runat="server" Text="Address(Hex)"></asp:Label>
    </th>
    <th width="100px" ID="TH2Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH2"  runat="server" Text="Address(Dec)"></asp:Label>
    </th>
     <th width="100px" ID="TH3Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH3"   runat="server" Text="Content(Hex)"></asp:Label>
    </th>
     <th width="300px" ID="TH4Title" class="tHStyleLeft" runat="server" >
        <asp:Label ID="TH4"  runat="server" Text="FiledName"></asp:Label>
    </th>
    <th width="300px" ID="TH5Title" class="tHStyleLeft" runat="server">
        <asp:Label ID="TH5"  runat="server" Text="FiledDescription"></asp:Label>
    </th>
   
    </tr>    
    <tr id="ContentTR" runat="server">
          
    <td class="tdLeft" width="100px"  style="border:solid #000 1px">
     <asp:Label ID="AddressHexText" runat="server"></asp:Label>
    </td>    
    <td class="tdLeft" width="100px"  style="border:solid #000 1px">
    <asp:Label ID="AddressDecText"   runat="server"></asp:Label>
    </td>
    <td class="tdLeft" width="100px"  style="border:solid #000 1px">
    <asp:TextBox ID="ContentText" BorderStyle="None" AutoPostBack="true" runat="server" 
            ontextchanged="ContentText_TextChanged"></asp:TextBox>
    </td>
   
    <td class="tdLeft" width="300px" style="border:solid #000 1px">
    
   <asp:Label ID="FiledNameText"   Text="" runat="server"></asp:Label>
    
    </td>  
      
    <td class="tdLeft" width="300px"  style="border:solid #000 1px">   
     <asp:Label ID="FiledDescriptionText"   Text="" runat="server"></asp:Label>     
    </td>
   <td> 
       <asp:RegularExpressionValidator ID="REVContentText" runat="server" 
    ControlToValidate="ContentText" ErrorMessage="Please Input two Hexadecimal characters" 
    ValidationExpression="" ForeColor="Red"></asp:RegularExpressionValidator>
   </td>
    </tr>
    </table>    
  
</div>

