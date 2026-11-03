<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.SignUp" Title="Transactional emails made easy for PayPal merchants" %>

<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">               
    <div id="maincontent">   
        <div class="sectiondivider"> 
            <div id="pagedescriptionbar"> 
                <span><asp:Label ID="PageDescription" Text="Sign Up with Us" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
            </div>
        </div>
        <br /><br />          
        <div style="text-align:center">   
            <fieldset style="min-width:500" class="editorsectionfieldset">       
        <div style="margin-left:10px; font-size:14px">
            <table>    
                <tr>
                    <td style="width: 221px; padding-top:5px">
                        First Name *</td>
                    <td style="width: 268px; padding-top:5px">
                        <asp:TextBox ToolTip="Enter full name" BorderWidth="1px" BorderColor="#cacaca" ID="txtFirstName" runat="server" MaxLength="50" Width="266px"></asp:TextBox></td>
                    <td style="width: 160px; padding-top:5px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 221px; padding-top:5px">
                        Last Name *</td>
                    <td style="width: 268px; padding-top:5px">
                        <asp:TextBox ToolTip="Enter full name" BorderWidth="1px" BorderColor="#cacaca" ID="txtLastName" runat="server" MaxLength="50" Width="266px"></asp:TextBox></td>
                    <td style="width: 160px; padding-top:5px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required" ControlToValidate="txtLastName"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 221px; padding-top:5px">
                        Email * </td>
                    <td style="width: 268px; padding-top:5px">
                        <asp:TextBox ID="txtEmail" BorderWidth="1px" BorderColor="#cacaca" runat="server" MaxLength="50" ReadOnly="True" Width="266px"></asp:TextBox></td>
                    <td style="width: 160px; padding-top:5px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required" ControlToValidate="txtEmail"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                <td></td>
                <td style="text-align:right"><asp:Button ID="btnSignUp" runat="server" 
                        Text="Sign Up" onclick="btnSignUp_Click" /> </td>
                <td></td>
                </tr>
            
            </table>
        </div> 
        <br />
              
    </fieldset>                             
        </div>             
    </div>    
</asp:Content>

