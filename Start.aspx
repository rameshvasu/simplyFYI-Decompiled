<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.Master" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Start" Title="Transactional emails made easy for PayPal merchants" %>

<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server"> 
    <div id="maincontent">  
        <div class="sectiondivider">       
            <div id="pagedescriptionbar">             
                <div style="padding-left:5px;padding-top:3px">Control Panel</div>            
            </div> 
        </div>
        <div style="float:left; padding-top:5px">
            <table cellspacing="15" width="700px"> 
                <tr style="height:50px">            
                    <td style="width: 56px;border-bottom:dotted 0px #bebebe;padding-top:5px; padding-bottom:5px; vertical-align:top">
                        <asp:Image ID="Image5" runat="server" ImageUrl="~/images/Start-Button.png" Width="45px" />   
                    </td>
                    <td valign="top" style="width: 360px;border-bottom:dotted 0px #bebebe">                
                        <asp:LinkButton ID="LinkButton2" Font-Bold="true" runat="server" OnClick="LinkButton2_Click">MANAGE FYIs</asp:LinkButton>   
                        <div  class="instruction">Manage FYI Apps for confirmation emails and newsletters</div>
                    </td>                
                </tr>                
                <tr style="height:50px">                        
                    <td style="width: 56px;border-bottom:dotted 0px #bebebe; padding-top:5px; padding-bottom:5px;vertical-align:top">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Add.png" />    
                    </td>
                    <td valign="top" style="width: 360px;border-bottom:dotted 0px #bebebe">                
                        <asp:LinkButton ID="LinkButton1" Font-Bold="true" runat="server" OnClick="LinkButton1_Click" >CREATE A FYI</asp:LinkButton>
                        <div class="instruction">Create FYIs that send engaging confirmation emails and newsletters to PayPal payers</div>
                    </td>                
                </tr>   
                <tr style="height:50px">            
                    <td style="width: 56px; height: 20px;border-bottom:dotted 0px #bebebe;padding-top:5px; padding-bottom:5px;vertical-align:top">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/images/Folder.png" /></td>
                    <td valign="top" style="width: 360px; height: 50px;border-bottom:dotted 0px #bebebe">
                        <asp:HyperLink ID="HyperLink4" Font-Bold="true" runat="server" NavigateUrl="~/Registration.aspx?action=2">MANAGE ACCOUNT SETTINGS</asp:HyperLink>
                        <div class="instruction">Manage account information, options, usage, and PayPal Notification FYIs</div>
                    </td>
                </tr>                                                                            
                <tr style="height:50px">            
                    <td style="width: 56px;border-bottom:dotted 0px #bebebe;padding-top:5px; padding-bottom:5px;vertical-align:top">
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/images/Graph2.png"
                            Width="47px" /></td>
                    <td valign="top" style="width: 360px;border-bottom:dotted 0px #bebebe">
                        <%--<asp:HyperLink ID="HyperLink1" Font-Bold="true" runat="server" NavigateUrl="~/MessageStatus.aspx">View Payer Engagement Analytics</asp:HyperLink>--%>
                        <asp:HyperLink ID="HyperLink1" Font-Bold="true" runat="server" NavigateUrl="~/MessageStatus.aspx">ANALYZE PAYER ENGAGEMENT</asp:HyperLink>
                        <div class="instruction">See how your PayPal payers engage with you</div>
                    </td>
                </tr>

                 <tr runat="server" id="ShipmentConfirmationMenuItem" style="height:50px">
                    <td style="width: 56px;border-bottom:dotted 0px #bebebe;padding-top:5px; padding-bottom:5px;vertical-align:top">
                        <asp:Image ID="Image10" runat="server" ImageUrl="~/images/deliveryvan.png"
                            Width="47px" /></td>
                    <td valign="top" style="width: 360px;border-bottom:dotted 0px #bebebe">
                        <asp:HyperLink ID="HyperLink7" Font-Bold="true" runat="server" NavigateUrl="~/ShipmentConfirmation.aspx">SEND SHIPMENT CONFIRMATION EMAILS</asp:HyperLink>
                        <div class="instruction">Send shipment confirmation emails with tracking number</div>
                    </td>
                </tr>

                <tr runat="server" id="DigitalCodeMenuItem" style="height:50px">
                    <td style="width: 56px;border-bottom:dotted 0px #bebebe;padding-top:5px; padding-bottom:5px;vertical-align:top">
                        <asp:Image ID="Image9" runat="server" Height="45px" ImageUrl="~/images/DigitalCode.jpg"
                            Width="47px" /></td>
                    <td valign="top" style="width: 360px;border-bottom:dotted 0px #bebebe">
                        <asp:HyperLink ID="HyperLink6" Font-Bold="true" runat="server" NavigateUrl="">VIEW DIGITAL CODES</asp:HyperLink>
                        <div class="instruction">View the redemption details of your digital codes</div>
                        <div><asp:HyperLink runat="server" NavigateUrl="~/DigitalCodes.aspx">Redemptions</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink runat="server" NavigateUrl="~/DigitalCodesFreeOffers.aspx">Free Offer Sign Ups</asp:HyperLink></div>
                    </td>
                </tr>
                <tr style="height:50px">            
                    <td style="width: 56px;border-bottom:dotted 0px #bebebe;padding-top:5px; padding-bottom:5px;vertical-align:top">
                        <asp:Image ID="Image7" runat="server" Height="45px" ImageUrl="~/images/contactlist.gif"
                            Width="47px" /></td>
                    <td valign="top" style="width: 360px;border-bottom:dotted 0px #bebebe">
                        <asp:HyperLink ID="HyperLink3" Font-Bold="true" runat="server" NavigateUrl="~/Payers.aspx">SEND NEWSLETTERS AND ANNOUNCEMENTS</asp:HyperLink>
                        <div class="instruction">Send email newsletters, announcements and promotions to your PayPal payers</div>
                    </td>
                </tr>
                <tr style="height:50px">            
                    <td style="width: 56px;border-bottom:dotted 0px #bebebe;padding-top:5px; padding-bottom:5px;vertical-align:top">
                        <asp:Image ID="Image8" runat="server" Height="45px" ImageUrl="~/images/transaction.gif"
                            Width="47px" /></td>
                    <td valign="top" style="width: 360px;border-bottom:dotted 0px #bebebe">
                        <asp:HyperLink ID="HyperLink5" Font-Bold="true" runat="server" NavigateUrl="~/PaymentTransactions.aspx">VIEW AND IMPORT PAYPAL PAYMENT TRANSACTIONS</asp:HyperLink>
                        <div class="instruction">View PayPal payment transaction details and import PayPal transaction history</div>
                    </td>
                </tr>
                <tr style="height:50px">            
                    <td style="width: 56px;border-bottom:dotted 0px #bebebe;padding-top:5px; padding-bottom:5px;vertical-align:top">
                        <asp:Image ID="Image6" runat="server" Height="45px" ImageUrl="~/images/Queued.png"
                            Width="47px" /></td>
                    <td valign="top" style="width: 360px;border-bottom:dotted 0px #bebebe">
                        <asp:HyperLink ID="HyperLink2" Font-Bold="true" runat="server" NavigateUrl="~/ManageQueuedMessages.aspx">MANAGE QUEUED MESSAGES</asp:HyperLink>
                        <div class="instruction">View or cancel messages that are queued for delivery</div>
                    </td>
                </tr>                 
                
                <tr style="height:50px">            
                    <td style="width: 56px;border-bottom:dotted 0px #bebebe;padding-top:5px; padding-bottom:5px;vertical-align:top">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/LeftArrow.png" /></td>
                    <td valign="top" style="width: 360px;border-bottom:dotted 0px #bebebe">
                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" Font-Bold="true" PostBackUrl="~/Start.aspx">LOGOUT</asp:LinkButton>
                        <div class="instruction">Log out of simplyFYI</div>
                    </td>
                </tr>
                
            </table>
        </div>
        <div runat="server" style="width:250px;float:right; padding-right:50px; padding-top:5px;">
            <div style="background-image:url(images/fuel-guage.jpg);background-size:55px;background-repeat:no-repeat;padding:3px 3px 3px 3px;width:213px; background-color:#ffffff; color:rgb(0, 175, 227); font-weight:normal; padding-left:80px;border: solid 1px #dbe1e6; font-size:24px;">UNITS REMAINING </div>
            <div id="startpagebalancediv" runat="server" style="padding:3px 3px 3px 3px; width:290px; background-color:#ffffff; font-weight:normal; border-left: solid 1px #dbe1e6;border-right: solid 1px #dbe1e6;border-bottom: solid 1px #dbe1e6; font-size:40px; text-align:center">
                Your account has 250 units available for use. To add more units click the "Topup" button.
            </div>
            
            <div>
                <div style="float:left">
                    <asp:Button ID="Topup" CssClass="NormalButton" runat="server" Text="Buy UNITS" OnClick="Topup_Click" />
                </div>
                <div style="float:right;padding-top:15px">
                    <asp:LinkButton ID="BalanceRefreshLink" runat="server" OnClick="BalanceRefreshLink_Click">Refresh</asp:LinkButton>
                </div>
                
            </div>
            
        </div>        
    </div>
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
