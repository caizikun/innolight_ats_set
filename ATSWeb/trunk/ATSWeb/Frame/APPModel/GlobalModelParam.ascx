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
<table  cellspacing="1" cellpadding="0">
    <tr> 
        <td class="tdStyleBorderBK" width="150px">名称</td> 
        <td><asp:TextBox ID="txtItemName" width="200px" height="22px" onkeydown="txtItemNameEnter()" runat="server"></asp:TextBox></td>
        <td>  
        &nbsp;          
           <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtItemName" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" Display="Dynamic" 
            ID="valeTxtItemName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>
           <asp:CompareValidator ID="valcTxtItemName" runat="server" 
                ErrorMessage="请输入字符串" ControlToValidate="txtItemName" Type="String" Display="Dynamic"
                ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
       </td>
    </tr>
    <tr> 
        <td class="tdStyleBorderBK" width="150px">别名</td> 
        <td><asp:TextBox ID="txtShowName" width="200px" height="22px" onkeydown="txtShowNameEnter()" runat="server"></asp:TextBox></td>
        <td>  
        &nbsp;          
           <asp:RequiredFieldValidator ID="valrTxtShowName" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtShowName" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ControlToValidate="txtShowName" ForeColor="Red" Display="Dynamic" 
            ID="valeTxtShowName" ValidationExpression="\S{1,30}" runat="server" ErrorMessage="字符长度1-30">
            </asp:RegularExpressionValidator>           
       </td>
    </tr>
    <tr> 
        <td class="tdStyleBorderBK" width="150px">参数类型</td> 
        <td>
        <asp:DropDownList ID="ItemTypeDropDownList" width="204px" height="28px" onkeydown="ItemTypeDropDownListEnter()" AutoPostBack="true" runat="server" 
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
        <td class="tdStyleBorderBK" width="150px">值</td> 
        <td><asp:TextBox ID="txtItemValue" width="200px" height="22px"  onkeydown="txtItemValueEnter()" 
                runat="server" MaxLength="255"></asp:TextBox></td>
        <td>
        &nbsp;
            <asp:RequiredFieldValidator ID="valrTxtItemValue"  runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtItemValue" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            &nbsp;<asp:RangeValidator ID="RangeValidatorItemValue" runat="server" 
                ControlToValidate="txtItemValue" ErrorMessage="请输入满足参数类型的值" 
                ForeColor="Red" Display="Dynamic"></asp:RangeValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorItemValue" 
                runat="server" ControlToValidate="txtItemValue" 
                ErrorMessage="" 
                ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
            
        </td>
    </tr>    
    <tr> 
        <td class="tdStyleBorderBK" width="150px">是否需要检查参数</td> 
        <td>
            <asp:DropDownList ID="ddlNeedCheckParams" width="204px" height="28px" onkeydown="ddlNeedCheckParamsEnter()"  runat="server">            
            <asp:ListItem>false</asp:ListItem>
            <asp:ListItem>true</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    

    <tr> 
        <td class="tdStyleBorderBK1" width="150px">可选参数</td> 
        <td><asp:TextBox ID="txtOptionalparams" width="198px" height="48px" onkeydown="txtOptionalparamsEnter()"  runat="server"  Rows="3" TextMode="MultiLine"></asp:TextBox></td>
        <td>
        &nbsp;
        <asp:RegularExpressionValidator ControlToValidate="txtOptionalparams" ForeColor="Red" Display="Dynamic" 
            ID="valeTxtOptionalparams" ValidationExpression="\S{0,4000}" runat="server" ErrorMessage="字符长度0-4000">
            </asp:RegularExpressionValidator>
        </td>
        <%--<td>
           <asp:RequiredFieldValidator ID="valrTxtOptionalparams" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtSpecMin" ForeColor="Red"  
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
                ErrorMessage="不能为空" ControlToValidate="txtSpecMax" ForeColor="Red"  
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
        <td class="tdStyleBorderBK1" width="150px">描述</td> 
        <td><asp:TextBox ID="txtItemDescription" width="198px" height="48px" onkeydown="txtItemDescriptionEnter()"  runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox></td>
        <td>
        &nbsp;
        <asp:RegularExpressionValidator ControlToValidate="txtItemDescription" ForeColor="Red" Display="Dynamic" 
            ID="valeTxtItemDescription" ValidationExpression="\S{0,200}" runat="server" ErrorMessage="字符长度0-200">
            </asp:RegularExpressionValidator>
        </td>
    </tr>
</table>

