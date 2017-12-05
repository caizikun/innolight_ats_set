<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PNActionList.ascx.cs" Inherits="ASCXPNActionList" %>

<style type="text/css">
    .style1
    {
        height: 18px;
    }
</style>

<div>    
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;">
<tr id="title">   
<th width="25px" height="23px" ID="TH0Title" class="tHStyleCenter" runat="server"></th>
   <th width="150px"  height="23px" ID="TH1Title" class="tHStyleCenter" runat="server" 
        align="center">
       <asp:Label ID="TH1" class="tdLeft" runat="server" Text="PN">PN</asp:Label>
   </th>
   <th width="105px" height="23px" ID="TH2Title" class="tHStyleCenter" runat="server" 
        align="center">
       <asp:Label ID="TH2" class="tdLeft" runat="server" Text="AddTestPlan?"></asp:Label>
   </th>
    <th width="105px" height="23px" ID="TH3Title" class="tHStyleCenter" runat="server" 
        align="center">
       <asp:Label ID="TH3" class="tdLeft" runat="server" Text="Edit?"></asp:Label>
   </th>

       <th width="105px" height="23px" ID="TH4Title" class="tHStyleCenter" runat="server" 
        align="center" visible="False">
       <asp:Label ID="TH4" class="tdLeft" runat="server" Text="ActionID"></asp:Label>
   </th>

</tr>
<tr id="ContentTR" runat="server">
        <td id="TD1" width="25px" height="20px" class="tdCenter" style="border:solid #000 1px" align="center">
            <asp:RadioButton ID="RadioButton1" runat="server" Assembly="GroupRadioButton" GroupName ="ChoosePN" 
                oncheckedchanged="RadioButton1_CheckedChanged" AutoPostBack="True" />
        </td>
        <td id="TD2" width="150px" height="20px" class="tdLeft" style="border:solid #000 1px">
            <asp:Label ID="LabelPN"  runat="server" Text="Label"></asp:Label>         
        </td>
        <td id="TD3" width="105px" height="20px" class="tdCenter" style="border:solid #000 1px">
            <asp:CheckBox ID="CheckBoxAddPlan" runat="server" Enabled="False" />
        </td> 

        <td id="TD4" width="105px" height="20px" class="tdCenter" style="border:solid #000 1px" 
            align="center">
          <asp:CheckBox ID="CheckBoxEdit" runat="server" Enabled="False" />
        </td>  
        
        <td id="TD5" width="105px" height="20px" class="tdLeft" style="border:solid #000 1px" Visible="False">
            <asp:Label ID="LabelActionID"  runat="server" Text="Label" Visible="False"></asp:Label>         
        </td>
             
</tr>
</table>
</div>