<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportHeaderInfo.ascx.cs" Inherits="Frame_ReportHeaderInfo" %>
        <script language="javascript" type="text/javascript">
            function txtItemNameEnter() {
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
<table cellspacing="1" cellpadding="0">
        <tr>
        <td class="tdStyleBorderBK" width="150px">名称</td>
        <td>
         <asp:TextBox ID="txtItemName" width="200px" height="22px" onkeydown="txtItemNameEnter()" runat="server"></asp:TextBox>
        </td>
        <td>
        &nbsp;
            <asp:RequiredFieldValidator ID="valrTxtItemName" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtItemName" ForeColor="Red"  
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtItemName" ForeColor="Red" Display="Dynamic" 
            ID="valeTxtItemName" ValidationExpression="\S{1,50}" runat="server" ErrorMessage="字符长度1-50">
            </asp:RegularExpressionValidator>
            
       </td>
       </tr>      
        <tr>
            <td class="tdStyleBorderBK1" width="150px">描述</td>
            <td>
             <asp:TextBox ID="txtItemDescription"  width="198px" height="48px" onkeydown="txtItemDescriptionEnter()"  runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
            &nbsp;
            <asp:RegularExpressionValidator ControlToValidate="txtItemDescription" ForeColor="Red" Display="Dynamic" 
            ID="RegularExpressionValidator2" ValidationExpression="\S{0,200}" runat="server" ErrorMessage="字符长度0-200">
            </asp:RegularExpressionValidator>
            </td>
        </tr>
       
</table>