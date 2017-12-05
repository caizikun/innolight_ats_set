<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PNActionList.ascx.cs" Inherits="ASCXPNActionList" %>

<style type="text/css">
    .style1
    {
        height: 18px;
    }
</style>

<div>    
<table cellspacing="0" cellpadding="0" style="  border:none; border-collapse:collapse;table-layout:fixed; width:490px;">
<tr>
    <td width="25px"></td>        
    <td width="210px"></td>        
    <td width="130px"></td>
    <td width="105px"></td> 
    <td width="20px"></td>      
    </tr>
<tr id="title">   
<td width="25px" height="23px" ID="TH0Title" class="tHStyleCenter1" runat="server"></td>
   <td width="210px"  height="23px" ID="TH1Title" class="tHStyleCenter1" runat="server" 
        align="center">
       <asp:Label ID="TH1" runat="server" Text="PN">品名</asp:Label>
   </td>
   <td width="130px" height="23px" ID="TH2Title" class="tHStyleCenter1" runat="server" 
        align="center">
       <asp:Label ID="TH2" runat="server" Text="可新增测试方案?"></asp:Label>
   </td>
    <td width="105px" height="23px" ID="TH3Title" class="tHStyleCenter1" runat="server" 
        align="center">
       <asp:Label ID="TH3" runat="server" Text="可编辑?"></asp:Label>
   </td>

       <td width="20px" height="23px" ID="TH4Title" class="tHStyleCenter1" runat="server" 
        align="center" visible="False">
       <asp:Label ID="TH4" runat="server" Text="ActionID"></asp:Label>
   </td>

</tr>
<tr id="ContentTR" runat="server">
        <td id="TD1" width="25px" height="20px" class="tdCenter" align="center">
            <asp:RadioButton ID="RadioButton1" runat="server" Assembly="GroupRadioButton" GroupName ="ChoosePN" 
                oncheckedchanged="RadioButton1_CheckedChanged" AutoPostBack="True" />
        </td>
        <td id="TD2" width="210px" height="20px" class="tdLeft">
            <asp:Label ID="LabelPN"  runat="server" Text="Label"></asp:Label>         
        </td>
        <td id="TD3" width="130px" height="20px" class="tdCenter">
            <asp:CheckBox ID="CheckBoxAddPlan" runat="server" Enabled="False" />
        </td> 

        <td id="TD4" width="105px" height="20px" class="tdCenter" 
            align="center">
          <asp:CheckBox ID="CheckBoxEdit" runat="server" Enabled="False" />
        </td>  
        
        <td id="TD5" width="20px" height="20px" class="tdLeft" Visible="False">
            <asp:Label ID="LabelActionID"  runat="server" Text="Label" Visible="False"></asp:Label>         
        </td>
             
</tr>
</table>
</div>