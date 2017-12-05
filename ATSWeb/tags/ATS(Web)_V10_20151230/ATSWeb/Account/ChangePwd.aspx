<%@ Page Title="" Language="C#" MasterPageFile="~/a_MasterPage.master" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="Account_ChangePwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ModulesNameListContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="UserInforContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
     <table id="ChangeFunc" class="showTableData" runat="Server">
        <tr>
            <td> * 原密码 </td> 
            <td> <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Width="148px"></asp:TextBox> </td>
        </tr>     

        <tr>
            <td> * 新密码 </td> 
            <td> <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" Width="148px"></asp:TextBox> </td>
            <td>            
            <asp:RequiredFieldValidator ID="valrTxtNewPwd" runat="server" 
            ErrorMessage="not null" ControlToValidate="txtNewPwd" ForeColor="Red"  
            SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtNewPwd" ForeColor="Red" 
            ID="valeTxtNewPwd" ValidationExpression="\S{6,50}" runat="server" ErrorMessage="密码长度6-50">
            </asp:RegularExpressionValidator>
            <asp:CompareValidator ID="valcTxtNewPwd" runat="server" 
            ErrorMessage="新密码与原密码相同!" ControlToValidate="txtNewPwd"
            ForeColor="Red" SetFocusOnError="True" ControlToCompare="txtPwd" Operator="NotEqual"></asp:CompareValidator>
            </td> 
        </tr>  

        <tr>
            <td> * 确认密码 </td> 
            <td> <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password" Width="148px"></asp:TextBox> </td>
            <td>
            
                <asp:RequiredFieldValidator ID="valrTxtConfirmPwd" runat="server" 
                ErrorMessage="not null" ControlToValidate="txtConfirmPwd" ForeColor="Red"  
                SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="txtConfirmPwd" ForeColor="Red" 
                ID="valeTxtConfirmPwd" ValidationExpression="\S{6,50}" runat="server" ErrorMessage="密码长度6-50">
                </asp:RegularExpressionValidator>
                <asp:CompareValidator ID="valcTxtConfirmPwd" runat="server" 
                ErrorMessage="密码前后输入，不一致！" ControlToCompare="txtNewPwd" 
                ForeColor="Red" ControlToValidate="txtConfirmPwd"></asp:CompareValidator>
            </td> 
        </tr>  
        

        <tr>
            <td></td> 
            <td> 
                <asp:Button ID="btnOK" runat="server" Text="确定" onclick="btnOK_Click" /> </td>
        </tr>

        <tr>
            <td></td> 
            <td> 
                <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
            </td>
        </tr> 
     </table>
</asp:Content>

