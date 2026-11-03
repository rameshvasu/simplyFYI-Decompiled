<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainTemplateWide.master" CodeBehind="TopupAutoSuccess.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.TopupAutoSuccess" %>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">     
    <div id="maincontent">
        <div class="sectiondivider"> 
            <div id="pagedescriptionbar"> 
                <span><asp:Label ID="PageDescription" Text="Auto Payments Enabled" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
            </div>
        </div>
        <div style="display:none">
             <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Initiate Auto Payment" CssClass="NormalButtonVariableSize" />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div>
       
        <div class="instructioncontainer">            
            <div id="divConfirmation" runat="server" class="instruction">You have successfully signed up for simplyFYI Auto Payments. The PayPall Billing Agreement ID is {BillingAgreementID}.<br /><br />           
            Whenever your simplyFYI UNITS balance goes below 300 UNITS, we will automatically purchase additional UNITS on your behalf and bill your PayPal account. You choose the quantity of additional UNITS that you want purchased automatically. 
            <br /><br />
            </div>                    
        </div>  
        <br /> 
        <div><a id=BackLink runat=server href="./Start.aspx">Back to Control Panel</a></div>     
    </div>    
</asp:Content>



