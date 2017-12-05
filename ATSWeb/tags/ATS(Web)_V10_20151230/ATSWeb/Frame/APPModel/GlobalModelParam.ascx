<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalModelParam.ascx.cs" Inherits="Frame_APPModel_GlobalModelParam" %>
 <script language="javascript" type="text/javascript">
     function txtItemNameEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=txtShowName.ClientID%>';
             var tempid = document.getElementById(tempt);
             if (tempid == null) {
                 return false;
             }
             else {
                 tempid.focus();
                 return false;
             }

         }
     }
     function txtShowNameEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=ItemTypeDropDownList.ClientID%>';
             var tempid = document.getElementById(tempt);
             if (tempid == null) {
                 return false;
             }
             else {
                 tempid.focus();
                 return false;
             }

         }
     }
     function ItemTypeDropDownListEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=txtItemValue.ClientID%>';
             var tempid = document.getElementById(tempt);
             if (tempid == null) {
                 return false;
             }
             else {
                 tempid.focus();
                 return false;
             }

         }
     }
     function txtItemValueEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=ddlNeedCheckParams.ClientID%>';
             var tempid = document.getElementById(tempt);
             if (tempid == null) {
                 return false;
             }
             else {
                 tempid.focus();
                 return false;
             }

         }
     }
     function ddlNeedCheckParamsEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=txtOptionalparams.ClientID%>';
             var tempid = document.getElementById(tempt);
             if (tempid == null) {
                 return false;
             }
             else {
                 tempid.focus();
                 return false;
             }

         }
     }
     function txtOptionalparamsEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=txtItemDescription.ClientID%>';
             var tempid = document.getElementById(tempt);
             if (tempid == null) {
                 return false;
             }
             else {
                 tempid.focus();
                 return false;
             }

         }
     }
     function txtItemDescriptionEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=txtItemName.ClientID%>';
             var tempid = document.getElementById(tempt);
             if (tempid == null) {
                 return false;
             }
             else {
                 tempid.focus();
                 return false;
             }

         }
     }
 </script>
<br />
<table  style=" border:solid #000 1px">
    <tr> 
        <td class="tdStyleBorderBK" width="150px">ItemName</td> 
        <td><asp:TextBox ID="txtItemName" Width="150px" onkeydown="txtItemNameEnter()" runat="server"></asp:TextBox></td>
        <td>            
           <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtItemName" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" 
            ID="valeTxtItemName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>
           <asp:CompareValidator ID="valcTxtItemName" runat="server" 
                ErrorMessage="please input a string" ControlToValidate="txtItemName" Type="String"
                ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
       </td>
    </tr>
    <tr> 
        <td class="tdStyleBorderBK" width="150px">ShowName</td> 
        <td><asp:TextBox ID="txtShowName" Width="150px" onkeydown="txtShowNameEnter()" runat="server"></asp:TextBox></td>
        <td>            
           <asp:RequiredFieldValidator ID="valrTxtShowName" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtShowName" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ControlToValidate="txtShowName" ForeColor="Red" 
            ID="valeTxtShowName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>           
       </td>
    </tr>
    <tr> 
        <td class="tdStyleBorderBK" width="150px">ItemType</td> 
        <td>
        <asp:DropDownList ID="ItemTypeDropDownList" Width="155px" onkeydown="ItemTypeDropDownListEnter()" AutoPostBack="true" runat="server" 
                onselectedindexchanged="ItemTypeDropDownList_SelectedIndexChanged">            
            <asp:ListItem>ArrayList</asp:ListItem>
            <asp:ListItem>byte</asp:ListItem>
            <asp:ListItem>UInt16</asp:ListItem>
             <asp:ListItem>double</asp:ListItem>
             <asp:ListItem>Bool</asp:ListItem>
            </asp:DropDownList>
        </td>
        
    </tr>

    <tr> 
        <td class="tdStyleBorderBK" width="150px">ItemValue</td> 
        <td><asp:TextBox ID="txtItemValue" Width="150px"  onkeydown="txtItemValueEnter()" 
                runat="server" MaxLength="255"></asp:TextBox></td>
        <td>
            <asp:RequiredFieldValidator ID="valrTxtItemValue"  runat="server" 
                ErrorMessage="not null" ControlToValidate="txtItemValue" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            &nbsp;<asp:RangeValidator ID="RangeValidatorItemValue" runat="server" 
                ControlToValidate="txtItemValue" ErrorMessage="please put a correct value" 
                ForeColor="Red"></asp:RangeValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorItemValue" 
                runat="server" ControlToValidate="txtItemValue" 
                ErrorMessage="" 
                ForeColor="Red"></asp:RegularExpressionValidator>
            
        </td>
    </tr>    
    <tr> 
        <td class="tdStyleBorderBK" width="150px">NeedCheckParams</td> 
        <td>
            <asp:DropDownList ID="ddlNeedCheckParams" Width="155px" onkeydown="ddlNeedCheckParamsEnter()"  runat="server">            
            <asp:ListItem>false</asp:ListItem>
            <asp:ListItem>true</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    

    <tr> 
        <td class="tdStyleBorderBK" width="150px">Optionalparams</td> 
        <td><asp:TextBox ID="txtOptionalparams" Width="150px" onkeydown="txtOptionalparamsEnter()"  runat="server"  Rows="3" TextMode="MultiLine"></asp:TextBox></td>
        <td><asp:RegularExpressionValidator ControlToValidate="txtOptionalparams" ForeColor="Red" 
            ID="valeTxtOptionalparams" ValidationExpression="\S{0,4000}" runat="server" ErrorMessage="字符长度0-4000">
            </asp:RegularExpressionValidator>
        </td>
        <%--<td>
           <asp:RequiredFieldValidator ID="valrTxtOptionalparams" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtSpecMin" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
           <asp:CompareValidator ID="valcTxtSpecMin" runat="server" 
                ErrorMessage="please input a double value" ControlToValidate="txtSpecMin" Type="Double"
                ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
       </td>--%>
    </tr>

    <%-- 不再存在该字段    
    <tr> 
        <td>SpecMax</td> 
        <td>        
            <asp:TextBox ID="txtSpecMax" runat="server" ></asp:TextBox>
        </td>
        <td>
           <asp:RequiredFieldValidator ID="valrTxtSpecMax" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtSpecMax" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
           <asp:CompareValidator ID="valcTxtSpecMax" runat="server" 
                ErrorMessage="please input a double value" ControlToValidate="txtSpecMax" Type="Double"
                ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
       </td>
    </tr>
    
    <tr> 
        <td>ItemSpecific</td> 
        <td>
            <asp:DropDownList ID="ddlItemSpecific" runat="server">
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    
    <tr> 
        <td>LogRecord</td> 
        <td>
            <asp:DropDownList ID="ddlLogRecord" runat="server">
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    
    <tr> 
        <td>Failbreak</td> 
        <td>
            <asp:DropDownList ID="ddlFailBreak" runat="server">
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    
    <tr> 
        <td>DataRecord</td> 
        <td>
            <asp:DropDownList ID="ddlDataRecord" runat="server">
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    --%>

    <tr> 
        <td class="tdStyleBorderBK" width="150px">ItemDescription</td> 
        <td><asp:TextBox ID="txtItemDescription" Width="150px" onkeydown="txtItemDescriptionEnter()"  runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox></td>
        <td><asp:RegularExpressionValidator ControlToValidate="txtItemDescription" ForeColor="Red" 
            ID="valeTxtItemDescription" ValidationExpression="\S{0,200}" runat="server" ErrorMessage="字符长度0-200">
            </asp:RegularExpressionValidator>
        </td>
    </tr>
</table>

