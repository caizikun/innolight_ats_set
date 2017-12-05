<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopoEquipParam.ascx.cs" Inherits="Frame_TestPlan_TopoEquipParam" %>

    <td style="border:solid #000 1px">    
    <asp:CheckBox ID="chkIDEquipParam" runat="server" />
    <asp:Label ID="lblItem" runat="server"></asp:Label>
    </td> 
    <td style="border:solid #000 1px">
    <asp:Label ID="lblItemType" runat="server"></asp:Label>    
    </td> 
    <td style="border:solid #000 1px">
    <asp:TextBox ID="txtItemValue" runat="server" MaxLength="255"></asp:TextBox> 
        <asp:RequiredFieldValidator ID="valrTxtItemValue" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtItemValue" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidatorItemValue" runat="server" 
    ErrorMessage="please input a correct number" 
    ControlToValidate="txtItemValue" ForeColor="Red"></asp:RangeValidator>
   <asp:RegularExpressionValidator ID="RegularExpressionValidatorItemValue" 
    runat="server" ControlToValidate="txtItemValue" ErrorMessage="" ForeColor="Red"></asp:RegularExpressionValidator>
    </td>    
    <td style="border:solid #000 1px">
    
    <asp:Label ID="lblItemDescription" runat="server"></asp:Label>    
    </td>