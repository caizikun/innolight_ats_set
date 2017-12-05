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
    <td width="180px" class="tdLeft">
    <asp:LinkButton ID="lnkItemName"
        runat="server">ItemName</asp:LinkButton>     
        </td>
    <td width="80px" class="tdLeft">
    <asp:Label ID="txtItemType" runat="server"  Enabled="False" >ItemType</asp:Label>    
    </td>    
    <td width="150px" class="tdLeft2">    
    <asp:TextBox ID="txtItemValue" runat="server" 
    onkeydown="txtItemValueEnter()"  Width="146px" Height="23px" MaxLength="255">ItemValue</asp:TextBox>
    <%--         
    <asp:Label ID="lblItemValue" runat="server">ItemValue</asp:Label>
    <uc1:AscxDDLTxt ID="AscxDDLTxt1" runat="server"/> --%> 
    </td>

    <td width="260px" class="tdLeft1">
        <asp:Label ID="txtItemDescription" runat="server"  ></asp:Label>
     </td>  
     <td>   
     &nbsp;   
    <asp:RangeValidator ID="RangeValidatorItemValue" runat="server"
    ControlToValidate="txtItemValue" ErrorMessage="请输入满足参数类型的值" 
    ForeColor="Red" Display="Dynamic"></asp:RangeValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidatorItemValue" 
    runat="server" ControlToValidate="txtItemValue"
    ErrorMessage="" 
    ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>

    </td>
    
