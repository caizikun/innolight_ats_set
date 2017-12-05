<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestPlanInfor.ascx.cs" Inherits="ASCXTestPlanInfor" %>
<script language="javascript" type="text/javascript">
    function TBItemNameEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit') {
            var tempt = '<%=TBSWVersion.ClientID%>';
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
    function TBSWVersionEnter() {
        if (event.keyCode == 13 && event.srcElement.type != 'submit')
         {

             var tempt = '<%=TBHwVersion.ClientID%>';
             var tempid = document.getElementById(tempt);
             if (tempid == null)
              {
                 return false;
             }
             else {
                 tempid.focus();
                 return false;
             }
          
        }
     }
     function TBHwVersionEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=TBUSBPort.ClientID%>';
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
     function TBUSBPortEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=DDIsChipIni.ClientID%>';
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

     function DDIsChipIniEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=DDIsEEPROMIni.ClientID%>';
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

     function DDIsEEPROMIniEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=DDIgnoreBackupCoef.ClientID%>';
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

     function DDIgnoreBackupCoefEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=DDSNCheck.ClientID%>';
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
     function DDSNCheckEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=DDPNCheck.ClientID%>';
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
     function DDPNCheckEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=DDSWCheck.ClientID%>';
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
     function DDSWCheckEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=DDIgnoreFlag.ClientID%>';
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
     function DDIgnoreFlagEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=Description.ClientID%>';
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
     function DescriptionEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=TextVersion.ClientID%>';
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

     function TextVersionEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=DDCDROn.ClientID%>';
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

     function DDCDROnEnter() {
         if (event.keyCode == 13 && event.srcElement.type != 'submit') {

             var tempt = '<%=TBItemName.ClientID%>';
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
        <td width="150px" class="tdStyleBorderBK">
            <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>      
        </td> 
        <td>
            <asp:TextBox ID="TBItemName" width="200px" height="22px" onkeydown="TBItemNameEnter()" runat="server" MaxLength="30"></asp:TextBox></td>
        <td>
        &nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="TBItemName" ForeColor="Red" 
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ErrorMessage="请输入字符串" ControlToValidate="TBItemName" Display="Dynamic"
                Operator="NotEqual" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator>
        <asp:RegularExpressionValidator ID="REV" ControlToValidate="TBItemName" runat="server" ErrorMessage="已存在" 
                ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
        </td>
        
    </tr>
    
    <tr> 
        <td width="150px" class="tdStyleBorderBK">
         <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td><asp:TextBox ID="TBSWVersion" width="200px" height="22px" onkeydown="TBSWVersionEnter()"  runat="server" MaxLength="30"></asp:TextBox></td>
        <td>
        &nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="TBSWVersion" 
                SetFocusOnError="True" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                ErrorMessage="请输入字符串" ControlToValidate="TBSWVersion" Display="Dynamic"
                SetFocusOnError="True" ForeColor="Red" Operator="NotEqual"></asp:CompareValidator>
            
        </td>
        
    </tr>
    <tr> 
        <td width="150px" class="tdStyleBorderBK"> 
         <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td><asp:TextBox ID="TBHwVersion" width="200px" height="22px" onkeydown="TBHwVersionEnter()"  runat="server" MaxLength="30"></asp:TextBox></td>
        <td>
        &nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="TBHwVersion" 
                SetFocusOnError="True" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator3" runat="server" 
                ErrorMessage="请输入字符串" SetFocusOnError="True" Display="Dynamic"
                ControlToValidate="TBHwVersion" ForeColor="Red" Operator="NotEqual"></asp:CompareValidator>
        </td>
    </tr>
     <tr> 
        <td width="150px" class="tdStyleBorderBK">
         <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td><asp:TextBox ID="TBUSBPort" width="200px" height="22px" onkeydown="TBUSBPortEnter()"  runat="server"></asp:TextBox></td>
        <td>
        &nbsp;
         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="TBUSBPort" 
                SetFocusOnError="True" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
         <asp:RangeValidator ID="RangeValidator1" runat="server" 
                ErrorMessage="请输入0-255之间的数字" 
                ControlToValidate="TBUSBPort" MaximumValue="255" Display="Dynamic"
                MinimumValue="0" SetFocusOnError="True" Type="Integer" ForeColor="Red"></asp:RangeValidator>
        </td>
    </tr>
    <tr> 
        <td width="150px" class="tdStyleBorderBK">
         <asp:Label ID="TH6" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td>
           <asp:DropDownList ID="DDIsChipIni" width="204px" height="28px" onkeydown="DDIsChipIniEnter()" runat="server">
           <asp:ListItem>False</asp:ListItem>
           <asp:ListItem>True</asp:ListItem>           
           </asp:DropDownList>
        </td>
    </tr>
    
    <tr> 
        <td width="150px" class="tdStyleBorderBK">
         <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td >
        <asp:DropDownList ID="DDIsEEPROMIni" width="204px" height="28px" onkeydown="DDIsEEPROMIniEnter()" runat="server">
        <asp:ListItem>False</asp:ListItem>
        <asp:ListItem>True</asp:ListItem>
        
   </asp:DropDownList>
        </td>
    </tr>
    
    <tr> 
        <td width="150px" class="tdStyleBorderBK">
         <asp:Label ID="TH8" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td>
           <asp:DropDownList  ID="DDIgnoreBackupCoef" width="204px" height="28px" onkeydown="DDIgnoreBackupCoefEnter()" runat="server">
           <asp:ListItem>False</asp:ListItem>
           <asp:ListItem>True</asp:ListItem>           
          </asp:DropDownList>
        </td>
    </tr>
       <tr> 
        <td width="150px" class="tdStyleBorderBK">
         <asp:Label ID="TH9" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td>
          <asp:DropDownList ID="DDSNCheck" width="204px" height="28px" onkeydown="DDSNCheckEnter()" runat="server">
           <asp:ListItem>False</asp:ListItem>
         <asp:ListItem>True</asp:ListItem>        
          </asp:DropDownList>
        </td>
    </tr>
        <tr> 
        <td width="150px" class="tdStyleBorderBK">
         <asp:Label ID="TH13" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td>
          <asp:DropDownList ID="DDPNCheck" width="204px" height="28px"  onkeydown="DDPNCheckEnter()" runat="server">
           <asp:ListItem>False</asp:ListItem>
         <asp:ListItem>True</asp:ListItem>        
          </asp:DropDownList>
        </td>
    </tr>
        <tr> 
        <td width="150px" class="tdStyleBorderBK">
         <asp:Label ID="TH14" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td>
          <asp:DropDownList ID="DDSWCheck" width="204px" height="28px" onkeydown="DDSWCheckEnter()" runat="server">
           <asp:ListItem>False</asp:ListItem>
         <asp:ListItem>True</asp:ListItem>        
          </asp:DropDownList>
        </td>
    </tr>
    <tr> 
        <td width="150px" class="tdStyleBorderBK">
         <asp:Label ID="TH10" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td>
          <asp:DropDownList ID="DDIgnoreFlag" width="204px" height="28px" onkeydown="DDIgnoreFlagEnter()" runat="server">
           <asp:ListItem>False</asp:ListItem>
         <asp:ListItem>True</asp:ListItem>        
          </asp:DropDownList>
        </td>
    </tr>
   
    <tr>
    <td width="150px" class="tdStyleBorderBK">
         <asp:Label ID="TH11" runat="server" Text="Label"></asp:Label>    
    </td> 
    <td style="text-overflow:ellipsis; white-space: nowrap;overflow: hidden;">
        <asp:TextBox ID="Description" width="200px" height="22px" onkeydown="DescriptionEnter()" runat="server" ></asp:TextBox>
    </td>
    <td>
    &nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="Description" 
                SetFocusOnError="True" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
            ControlToValidate="Description" runat="server" 
            ErrorMessage="RegularExpressionValidator" Display="Dynamic" 
            ForeColor="Red" ValidationExpression="(\w|\W){1,200}">字符长度1-200</asp:RegularExpressionValidator>
        </td>
    </tr>
    
    <tr>
    <td width="150px" class="tdStyleBorderBK">
         <asp:Label ID="TH12" runat="server" Text="Label"></asp:Label>    
    </td> 
    <td style="text-overflow:ellipsis; white-space: nowrap;overflow: hidden;">
        <asp:TextBox ID="TextVersion" width="200px" height="22px"  runat="server" onkeydown="TextVersionEnter()"></asp:TextBox>
    </td>   
    </tr>

    <tr> 
        <td width="150px" class="tdStyleBorderBK">
         <asp:Label ID="TH15" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td>
          <asp:DropDownList ID="DDCDROn" width="204px" height="28px" onkeydown="DDCDROnEnter()" runat="server">
           <asp:ListItem>False</asp:ListItem>
           <asp:ListItem>True</asp:ListItem>        
          </asp:DropDownList>
        </td>
    </tr>
</table>




