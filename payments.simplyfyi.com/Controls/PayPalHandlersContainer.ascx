<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayPalHandlersContainer.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.PayPalNotificationHandlersContainer" %>

<%@ Register src="PayPalHandlerSettings.ascx" tagname="PayPalHandlerSettings" tagprefix="uc1" %>

<%@ Register src="PayPalHandlerSettingsForMobile.ascx" tagname="PayPalHandlerSettingsForMobile" tagprefix="uc2" %>

<%@ Register src="PayPalHandlerSettingsWithDynamicSelector.ascx" tagname="PayPalHandlerSettingsWithDynamicSelector" tagprefix="uc3" %>

<%@ Register src="PayPalHandlerSettingsWithDynamicSelectorForCart.ascx" tagname="PayPalHandlerSettingsWithDynamicSelectorForCart" tagprefix="uc4" %>

<%@ Register src="PayPalHandlerSettingsWithDynamicSelectorForSubscription.ascx" tagname="PayPalHandlerSettingsWithDynamicSelectorForSubscription" tagprefix="uc5" %>

<script type="text/javascript">
    function domouseover(tabid) {
        var tabclass = document.getElementById(tabid).getAttribute("class");

        if (tabclass != "engagenowverticaltab-ul-li-a-selected") {
            document.getElementById(tabid).setAttribute("class", "engagenowverticaltab-ul-li-a-hover");
        }
    }

    function domouseout(tabid) {
        var tabclass = document.getElementById(tabid).getAttribute("class");

        if (tabclass != "engagenowverticaltab-ul-li-a-selected") {
            document.getElementById(tabid).setAttribute("class", "engagenowverticaltab-ul-li-a");
        }
    }

    function showPaymentReceivedHandler()
    {        
        $("#subscrpaymentdiv").show();
        $("#subscrsignupdiv").hide();

        $("#subscrpaymentshowhidediv").removeClass('tabbutton').addClass('tabbuttonactive');
        $("#subscrsignupshowhidediv").removeClass('tabbuttonactive').addClass('tabbutton');
        $("#ipninstructionshowhidediv").hide();
    }

    function showNewSubscriberHandler()
    {
        $("#subscrpaymentdiv").hide();
        $("#subscrsignupdiv").show();
       
        $("#subscrsignupshowhidediv").removeClass('tabbutton').addClass('tabbuttonactive');
        $("#subscrpaymentshowhidediv").removeClass('tabbuttonactive').addClass('tabbutton');

        $("#ipninstructionshowhidediv").show();
    }

    function showIPNInstruction()
    {        
        if ($("#ipninstruction-heading").text().indexOf("read") >= 0)
            $("#ipninstruction-heading").text("Enable PayPal IPN to send welcome emails to new subscribers - Click to close");
        else
            $("#ipninstruction-heading").text("Enable PayPal IPN to send welcome emails to new subscribers- Click to read");

        $("#ipninstructiondiv").toggle();
    }

</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="100%">
            <tr>
                <td style="width:200px" valign="top">	
                    <div id="engagenowverticaltabdiv" class="engagenowverticaltab" style="float:left;margin-left:2px; margin-right:15px; padding-top:0px;">
                        <ul class="engagenowverticaltab-ul">   
                            <li class="engagenowverticaltab-ul-li"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab5' 
                                    OnClientClick="javascript:settabfocus('tab5');" 
                                    PostBackUrl="/payments/Registration.aspx?action=2&event=5" onclick="tab5_Click">Single Item Payments</asp:LinkButton></li>                                                                      
                            <li class="engagenowverticaltab-ul-li" style="display:block"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab1'                                     
                                    PostBackUrl="/payments/Registration.aspx?action=2&event=1" onclick="tab1_Click">Cart Payments</asp:LinkButton></li>                            
                            <li class="engagenowverticaltab-ul-li" style="display:block"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab2'                                     
                                    PostBackUrl="/payments/Registration.aspx?action=2&event=2" onclick="tab2_Click">Subscription Payments</asp:LinkButton></li>                            
                            <li class="engagenowverticaltab-ul-li" style="display:block"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab3' 
                                    OnClientClick="javascript:settabfocus('tab3');" 
                                    PostBackUrl="/payments/Registration.aspx?action=2&event=3" onclick="tab3_Click">Mobile Payments</asp:LinkButton></li>                             
                            <li class="engagenowverticaltab-ul-li" style="display:block"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab6' 
                                    OnClientClick="javascript:settabfocus('tab6');" 
                                    PostBackUrl="/payments/Registration.aspx?action=2&event=3" onclick="tab6_Click">VT Payments</asp:LinkButton></li>                            
                             <li class="engagenowverticaltab-ul-li" style="display:block"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab7' 
                                    OnClientClick="javascript:settabfocus('tab7');" 
                                    PostBackUrl="/payments/Registration.aspx?action=2&event=3" onclick="tab7_Click">Invoice / Send Money</asp:LinkButton></li> 
                            <li class="engagenowverticaltab-ul-li" style="display:block"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab8' 
                                    OnClientClick="javascript:settabfocus('tab8');" 
                                    PostBackUrl="/payments/Registration.aspx?action=2&event=3" onclick="tab8_Click">Express Checkout</asp:LinkButton></li> 
                            <li class="engagenowverticaltab-ul-li" style="display:none"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab4' 
                                    OnClientClick="javascript:settabfocus('tab4');" 
                                    PostBackUrl="/payments/Registration.aspx?action=2&event=4" onclick="tab4_Click">Opt-In By Text</asp:LinkButton></li>                                                       

                        </ul>                               
                    </div>            	    		            
                </td>
                <td style="vertical-align:top; padding-top:3px; padding-left:10px">
                    <div style="border:1px solid #dadada; width:700px; padding: 10px 20px 20px 10px; box-shadow: -5px -5px 5px #dadada;">                                                                          
                        <asp:MultiView ID="mvEventHandlers" runat="server">
                            <asp:View ID="SingleItemHandlerView" runat="server">
                                <div style="width:690px">
                                    <div class="instructioncontainer">                               
                                        <div class="instruction-big">Handle "Buy Now" or "Pay Now" or "Donate" Payments</div>
                                        <br />
                                        <div class="instruction">
                                            <ul class="instruction-list">                                                               
                                                <li>Select a FYI to run when this payment transaction occurs.</li>                                                                                 
                                                <li>When ready to process live payment transactions, set the options under "Working with Live Production Data".</li>                                                                                                                 
                                                <li><span style="padding-left:5px">Run a FYI based on item name or item number.</span></li>
                                            </ul>                                        
                                        </div>
                                    </div>
                                </div>
                                
                                <br />                                
                                <uc3:PayPalHandlerSettingsWithDynamicSelector ID="PayPalHandlerSettings4WebAccept" runat="server" />
                            </asp:View>
                            <asp:View ID="CartHandlerView" runat="server">
                                <div style="width:690px">
                                    <div class="instructioncontainer">
                                        <div class="instruction-big">Handle Shopping Cart Payments</div>
                                        <br />
                                        <div class="instruction">
                                            <ul class="instruction-list">                                                                
                                                <li>Select a FYI to run when this payment transaction occurs.</li>                                                                                 
                                                <li>When ready to process live payment transactions, set the options under "Working with Live Production Data".</li>                                                                                                                 
                                                <li><span style="padding-left:5px">Run a FYI based on item name or item number in the cart.</span></li>
                                            </ul>                                        
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <%--<uc1:PayPalHandlerSettings ID="PayPalHandlerSettings4ShoppingCart" runat="server" />--%>
                                <uc4:PayPalHandlerSettingsWithDynamicSelectorForCart ID="PayPalHandlerSettings4ShoppingCart" runat="server" />
                            </asp:View>
                            <asp:View ID="SubscriptionHandlerView" runat="server">
                                <div style="width:690px">
                                    <div class="instructioncontainer">
                                        <div class="instruction-big">Handle Subscription Payments</div>
                                        <br />
                                        <div class="instruction">
                                            <ul class="instruction-list">  
                                               <li>Click on a transaction trigger: 'Payment Received' or 'Subscriber Signup'</li>                                                             
                                               <li>Select a FYI to run when this payment transaction occurs.</li>                                                                                 
                                               <li>When ready to process live payment transactions, set the options under "Working with Live Production Data".</li>                                                                                                                 
                                            </ul>                                        
                                        </div>
                                    </div>
                                </div>
                                <br />                                
                                <span>
                                    <span>Select trigger: </span>
                                    <span onclick="showPaymentReceivedHandler();" class="tabbuttonactive instruction" id="subscrpaymentshowhidediv" style="cursor:pointer; margin-top:10px">Payment Received</span> 
                                    | <span onclick="showNewSubscriberHandler();" class="tabbutton instruction" id="subscrsignupshowhidediv" style="cursor:pointer; margin-top:10px">Subscriber Signup</span >
                                </span>
                                <br /><br />
                                <div id="subscrpaymentdiv" style="background-color:transparent; border:solid 1px #dadada; padding-left:5px; padding-right:5px; margin-left:10px; margin-top:5px; display:block">                                    
                                    <%--<uc1:PayPalHandlerSettings ID="PayPalHandlerSettings4Subscription" runat="server" />  --%>   
                                    <uc5:PayPalHandlerSettingsWithDynamicSelectorForSubscription ID="PayPalHandlerSettings4Subscription" runat="server" />                                                           
                                </div>
                                <div id="ipninstructionshowhidediv" onclick="showIPNInstruction();" class="instructioncontainer-green instruction" style="cursor:pointer; margin-left:10px; width:668px; display:none">
                                        <strong><span id="ipninstruction-heading">Enable PayPal IPN to send welcome emails to new subscribers - Click to read</span></strong>
                                        <div id="ipninstructiondiv" style="padding-left:0px; display:none">
                                            <div style="padding-top:10px">
                                                <div > 
                                                    <div class="instruction">
                                                        In order for PayPal to notify simplyFYI that a new subscriber has signed up, you have to enable PayPal's Instant Payment Notification (IPN). 
                                                        <u>simplyFYI will not be able to send welcome emails to your new subscribers without you completing this step</u>. <br /><br />
                                                        Login to your PayPal merchant account to enable IPN. You will be required to provide PayPal a "Notification URL". Please use your Notification URL below. <a href="http://www.simplyfyi.com/Tutorials/TutorialConfigurePayPalIPN/TutorialConfigurePayPalIPN4Subscriptions.aspx" target="_blank">Learn how to enable PayPal IPN.</a>
                                                        <br /><br /><strong>NOTE:</strong> You do not have to enable IPN if you are interested in sending payment confirmation emails only with the "Payment Received" trigger.
                                                    </div>  
                                                    <br />                                         
                                                    <div class="instruction">Your Notification URL:</div>
                                                        <table width="100%" style="background:#fff; border:solid 1px #888; padding:5px">                                                                                  
                                                            <tr>                                       
                                                                <td style="width: 600px; padding-top:5px">                                                
                                                                    <asp:Label Font-Size="Small" ID="lblIPNUrl" runat="server" />                                            
                                                                </td>                    
                                                            </tr> 
                                                        </table>                                                                                                             
                                                    <br />
                                                </div>
                                            </div>                                                
                                        </div>                                        
                                    </div>
                                <div id="subscrsignupdiv" style="background-color:transparent; border:solid 1px #dadada; padding-left:5px; padding-right:5px; margin-left:10px; margin-top:5px; display:none">                                    
                                     
                                    <div>                                                                                   
                                        <%--<uc1:PayPalHandlerSettings ID="PayPalHandlerSettings4NewSubscriptionSignup" runat="server" /> --%>
                                        <uc5:PayPalHandlerSettingsWithDynamicSelectorForSubscription ID="PayPalHandlerSettings4NewSubscriptionSignup" runat="server" />                                                           
                                    </div>
                                                                                                                           
                            </asp:View>
                            <asp:View ID="MobileHandlerView" runat="server"> 
                                <div style="width:690px"> 
                                    <div class="instructioncontainer">
                                        <div class="instruction-big">Handle Mobile (PayPal Here) Payments</div>
                                        <br />
                                        <div class="instruction" style="display:none">
                                            <ul class="instruction-list">                        
                                                <li>Select a FYI to run when this payment transaction occurs.</li>                                                                                 
                                                <li>When ready to process live payment transactions, set the options under "Working with Live Production Data".</li>                                                                                                                 
                                            </ul>                                        
                                        </div>
                                    </div>
                                </div>
                                <br />                              
                                <uc2:PayPalHandlerSettingsForMobile ID="PayPalHandlerSettings4PayPalHere" runat="server" />                                
                            </asp:View>
                            <asp:View ID="VTTerminalHandlerView" runat="server">
                                <div style="width:690px">
                                    <div class="instructioncontainer">                               
                                        <div class="instruction-big">Handle "Virtual Terminal" Payments</div>
                                        <br />
                                        <div class="instruction">
                                            <ul class="instruction-list">                                                               
                                                <li>Select a FYI to run when this payment transaction occurs.</li>                                                                                 
                                                <li>When ready to process live payment transactions, set the options under "Working with Live Production Data".</li>                                                                                                                                                                 
                                            </ul>                                        
                                        </div>
                                    </div>
                                </div>
                                
                                <br />                                
                                <uc1:PayPalHandlerSettings ID="PayPalHandlerSettings4VirtualTerminal" runat="server" />
                            </asp:View>
                            <asp:View ID="SendMoneyHandlerView" runat="server">
                                <div style="width:690px">
                                    <div class="instructioncontainer">                               
                                        <div class="instruction-big">Handle Invoice and "Send Money" Payments<span class="betafeature">BETA</span></div>
                                        <br />
                                        <div class="instruction">
                                            <ul class="instruction-list">                                                               
                                                <li>Select a FYI to run when this payment transaction occurs.</li>                                                                                 
                                                <li>When ready to process live payment transactions, set the options under "Working with Live Production Data".</li>                                                                                                                                                                 
                                            </ul>                                        
                                        </div>
                                    </div>
                                </div>
                                
                                <br />                                
                                <uc1:PayPalHandlerSettings ID="PayPalHandlerSettings4SendMoney" runat="server" />
                            </asp:View>
                            <asp:View ID="ExpressCheckoutHandlerView" runat="server">
                                <div style="width:690px">
                                    <div class="instructioncontainer">                               
                                        <div class="instruction-big">Handle Express Checkout Payments<span class="betafeature">BETA</span></div>
                                        <br />
                                        <div class="instruction">
                                            <ul class="instruction-list">                                                               
                                                <li>Select a FYI to run when this payment transaction occurs.</li>                                                                                 
                                                <li>When ready to process live payment transactions, set the options under "Working with Live Production Data".</li>                                                                                                                                                                 
                                            </ul>                                        
                                        </div>
                                    </div>
                                </div>
                                
                                <br />                                
                                <uc1:PayPalHandlerSettings ID="PayPalHandlerSettings4ExpressCheckout" runat="server" />
                            </asp:View>
                        </asp:MultiView>
                    </div>                    
                </td>
                <td valign="top">
                    &nbsp;</td>
            </tr>
        </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        
        <br/>                        
                    
        <asp:HiddenField ID="SelectedChannelHiddenInput" runat="server" />
