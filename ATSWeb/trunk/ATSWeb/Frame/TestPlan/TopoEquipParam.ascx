<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopoEquipParam.ascx.cs" Inherits="Frame_TestPlan_TopoEquipParam" %>

    <td width="160px" class="tdLeft">    
    <asp:Label ID="lblItem" runat="server"></asp:Label>
    </td> 
    <td width="80px" class="tdLeft">
    <asp:Label ID="lblItemType" runat="server"></asp:Label>    
    </td> 
    <td width="150px" class="tdLeft2">
    <asp:TextBox ID="txtItemValue" runat="server" MaxLength="255" Width="146px" Height="23px"></asp:TextBox> 
    </td>    
    <td width="260px" class="tdLeft1">   
    <asp:Label ID="lblItemDescription" runat="server"></asp:Label>    
    </td>
    <td>
    &nbsp;
        <asp:RequiredFieldValidator ID="valrTxtItemValue" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="txtItemValue" ForeColor="Red"  
            SetFocusOnError="True" Display="Dynamic" ></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidatorItemValue" runat="server" 
            ErrorMessage="请输入满足参数类型的值" 
            ControlToValidate="txtItemValue" ForeColor="Red" Display="Dynamic"></asp:RangeValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidatorItemValue" 
            runat="server" ControlToValidate="txtItemValue" ErrorMessage="" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
    </td>