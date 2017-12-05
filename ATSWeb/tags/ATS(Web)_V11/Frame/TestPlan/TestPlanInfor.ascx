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
<table style=" border:solid #000 1px">
    <tr> 
        <td width="100px" class="tdStyleBorderBK">
            <asp:Label ID="TH2" runat="server" Text="Label"></asp:Label>      
        </td> 
        <td width="130px"  ><asp:TextBox ID="TBItemName" onkeydown="TBItemNameEnter()" runat="server" MaxLength="30"></asp:TextBox></td>
        <td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="not null" ControlToValidate="TBItemName" ForeColor="Red" 
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ErrorMessage="please input a string" ControlToValidate="TBItemName" 
                Operator="NotEqual" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator>
        <asp:RegularExpressionValidator ID="REV" ControlToValidate="TBItemName" runat="server" ErrorMessage=" already existed" 
                ForeColor="Red"></asp:RegularExpressionValidator>
        </td>
        
    </tr>
    
    <tr> 
        <td width="100px" class="tdStyleBorderBK">
         <asp:Label ID="TH3" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td width="130px"><asp:TextBox ID="TBSWVersion" onkeydown="TBSWVersionEnter()"  runat="server" MaxLength="30"></asp:TextBox></td>
        <td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="not null" ControlToValidate="TBSWVersion" 
                SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                ErrorMessage="please input a string" ControlToValidate="TBSWVersion" 
                SetFocusOnError="True" ForeColor="Red" Operator="NotEqual"></asp:CompareValidator>
            
        </td>
        
    </tr>
    <tr> 
        <td width="100px" class="tdStyleBorderBK"> 
         <asp:Label ID="TH4" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td width="130px"><asp:TextBox ID="TBHwVersion"  onkeydown="TBHwVersionEnter()"  runat="server" MaxLength="30"></asp:TextBox></td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="not null" ControlToValidate="TBHwVersion" 
                SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator3" runat="server" 
                ErrorMessage="please input a string" SetFocusOnError="True" 
                ControlToValidate="TBHwVersion" ForeColor="Red" Operator="NotEqual"></asp:CompareValidator>
        </td>
    </tr>
     <tr> 
        <td width="100px" class="tdStyleBorderBK">
         <asp:Label ID="TH5" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td width="130px"><asp:TextBox ID="TBUSBPort" onkeydown="TBUSBPortEnter()"  runat="server"></asp:TextBox></td>
        <td>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="not null" ControlToValidate="TBUSBPort" 
                SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
         <asp:RangeValidator ID="RangeValidator1" runat="server" 
                ErrorMessage="please input a number between 0-255" 
                ControlToValidate="TBUSBPort" MaximumValue="255" 
                MinimumValue="0" SetFocusOnError="True" Type="Integer" ForeColor="Red"></asp:RangeValidator>
        </td>
    </tr>
    <tr> 
        <td width="100px" class="tdStyleBorderBK">
         <asp:Label ID="TH6" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td width="130px">
           <asp:DropDownList ID="DDIsChipIni" onkeydown="DDIsChipIniEnter()" runat="server" Width="152px">
           <asp:ListItem>False</asp:ListItem>
           <asp:ListItem>True</asp:ListItem>           
           </asp:DropDownList>
        </td>
    </tr>
    
    <tr> 
        <td width="100px" class="tdStyleBorderBK">
         <asp:Label ID="TH7" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td width="130px">
        <asp:DropDownList ID="DDIsEEPROMIni" Width="152px" onkeydown="DDIsEEPROMIniEnter()" runat="server">
        <asp:ListItem>False</asp:ListItem>
        <asp:ListItem>True</asp:ListItem>
        
   </asp:DropDownList>
        </td>
    </tr>
    
    <tr> 
        <td width="100px" class="tdStyleBorderBK">
         <asp:Label ID="TH8" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td width="130px">
           <asp:DropDownList  ID="DDIgnoreBackupCoef" Width="152px" onkeydown="DDIgnoreBackupCoefEnter()" runat="server">
           <asp:ListItem>False</asp:ListItem>
           <asp:ListItem>True</asp:ListItem>           
          </asp:DropDownList>
        </td>
    </tr>
       <tr> 
        <td width="100px" class="tdStyleBorderBK">
         <asp:Label ID="TH9" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td width="130px">
          <asp:DropDownList ID="DDSNCheck" Width="152px" onkeydown="DDSNCheckEnter()" runat="server">
           <asp:ListItem>False</asp:ListItem>
         <asp:ListItem>True</asp:ListItem>        
          </asp:DropDownList>
        </td>
    </tr>
        <tr> 
        <td width="100px" class="tdStyleBorderBK">
         <asp:Label ID="TH13" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td width="130px">
          <asp:DropDownList ID="DDPNCheck" Width="152px"  onkeydown="DDPNCheckEnter()" runat="server">
           <asp:ListItem>False</asp:ListItem>
         <asp:ListItem>True</asp:ListItem>        
          </asp:DropDownList>
        </td>
    </tr>
        <tr> 
        <td width="100px" class="tdStyleBorderBK">
         <asp:Label ID="TH14" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td width="130px">
          <asp:DropDownList ID="DDSWCheck" Width="152px"  onkeydown="DDSWCheckEnter()" runat="server">
           <asp:ListItem>False</asp:ListItem>
         <asp:ListItem>True</asp:ListItem>        
          </asp:DropDownList>
        </td>
    </tr>
    <tr> 
        <td width="100px" class="tdStyleBorderBK">
         <asp:Label ID="TH10" runat="server" Text="Label"></asp:Label>    
        </td> 
        <td width="130px">
          <asp:DropDownList ID="DDIgnoreFlag" Width="152px"  onkeydown="DDIgnoreFlagEnter()" runat="server">
           <asp:ListItem>False</asp:ListItem>
         <asp:ListItem>True</asp:ListItem>        
          </asp:DropDownList>
        </td>
    </tr>
   
    <tr>
    <td width="100px" class="tdStyleBorderBK">
         <asp:Label ID="TH11" runat="server" Text="Label"></asp:Label>    
    </td> 
    <td width="130px" style="text-overflow:ellipsis; white-space: nowrap;overflow: hidden;">
        <asp:TextBox ID="Description" onkeydown="DescriptionEnter()" runat="server" ></asp:TextBox>
    </td>
    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="not null" ControlToValidate="Description" 
                SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
            ControlToValidate="Description" runat="server" 
            ErrorMessage="RegularExpressionValidator" Display="Dynamic" 
            ForeColor="Red" ValidationExpression="(\w|\W){1,200}">the maximum namber is 200 characters</asp:RegularExpressionValidator>
        </td>
    </tr>
    
    <tr>
    <td width="100px" class="tdStyleBorderBK">
         <asp:Label ID="TH12" runat="server" Text="Label"></asp:Label>    
    </td> 
    <td width="130px" style="text-overflow:ellipsis; white-space: nowrap;overflow: hidden;">
        <asp:TextBox ID="TextVersion"   runat="server" ></asp:TextBox>
    </td>   
    </tr>
</table>




