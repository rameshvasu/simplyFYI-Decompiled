<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.Master" AutoEventWireup="true" CodeBehind="SubscriptionTriggers.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.SubscriptionTriggers" Title="Transactional emails made easy for PayPal merchants" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"%>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">   
    <div id="maincontent">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <div class="sectiondivider">
        <div id="pagedescriptionbar"> 
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>       
        
        </div>
        <div class="instructioncontainer">
            <div class="instruction">
                <div class="instruction-big">Choose a subscription trigger for this FYI</div>
                <br /><div class="instruction">
                    <p>With simplyFYI, you can automatically send a custom welcome email to the subscriber on sign up.</p>
                    <p>You can also automatically send a custom payment confirmation email when the subscriber makes subscription payments.</p>
                </div>                 
            </div>                     
        </div>
        <br />
        <table>            
            <tr>               
                <td valign="middle"> 
                    <asp:DropDownList ID="DDSubscriptionTrigger" runat="server" Width="525px">
                        <asp:ListItem Selected="True">New subscriber signup - Create welcome email</asp:ListItem>
                        <asp:ListItem >Subscription payment received - Create payment confirmation email</asp:ListItem>                        
                    </asp:DropDownList>                  
                </td>
                <td align="center" style="padding-top:12px; padding-left:10px">
                    <asp:Button  ID="Button1" runat="server" Text="Next" OnClick="Button1_Click" TabIndex="1"/>
                </td>                           
            </tr>
            <tr>
                <td colspan="3">
                    <a href="./SelectChannel.aspx">Go Back</a>
                </td>
            </tr>           
        </table>                     
     <br />               
    </div>           
</asp:Content>

