<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModelParams.ascx.cs" Inherits="Frame_TestPlan_ModelParams" %>
<%@ Register src="~/Frame/AscxDDLTxt.ascx" tagname="AscxDDLTxt" tagprefix="uc1" %>
    <script language="javascript" type="text/javascript">

        function txtItemValueEnter() {
            if (event.keyCode == 13 && event.srcElement.type != 'submit') {
                var tempt = '<%=txtItemValue.ClientID%>';
                var tempid = document.getElementById(tempt);

                    tempid.focus();
                    return false;
               
            }
        }
    </script>
    <td style="border:solid #000 1px">
    <asp:CheckBox ID="chkIDModelParam" runat="server" />
    <asp:LinkButton ID="lnkItemName"
        runat="server">ItemName</asp:LinkButton>     
        </td>
    <td style="border:solid #000 1px">
    <asp:Label ID="txtItemType" runat="server"  Enabled="False" >ItemType</asp:Label>    
    </td>    
    <td style="border:solid #000 1px">    
    <asp:TextBox ID="txtItemValue" runat="server" 
    onkeydown="txtItemValueEnter()"  Width="260px" MaxLength="255">ItemValue</asp:TextBox>
    <%--         
    <asp:Label ID="lblItemValue" runat="server">ItemValue</asp:Label>
    <uc1:AscxDDLTxt ID="AscxDDLTxt1" runat="server"/> --%> 
    </td>

    <td style="border:solid #000 1px">
        <asp:Label ID="txtItemDescription" runat="server"  ></asp:Label>
            
            <asp:RangeValidator ID="RangeValidatorItemValue" runat="server"
    ControlToValidate="txtItemValue" ErrorMessage="please put a correct value" 
    ForeColor="Red"></asp:RangeValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidatorItemValue" 
    runat="server" ControlToValidate="txtItemValue"
    ErrorMessage="" 
    ForeColor="Red"></asp:RegularExpressionValidator>

    </td>
    
