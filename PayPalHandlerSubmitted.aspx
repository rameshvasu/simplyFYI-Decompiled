<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="PayPalHandlerSubmitted.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.PayPalHandlerSubmitted" Title="Transactional emails made easy for PayPal merchants" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="sectiondivider"> 
        <div id="pagedescriptionbar"> 
            <span><asp:Label ID="PageDescription" Text="PayPal Notification FYI has been saved" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
        </div>
    </div>
    <div style="margin-left:10px;margin-top:10px;">
        <div class="instructioncontainer">
            <div class="instruction-big" style="display:none">Status Message</div>
            <div class="instruction" style="display:none">
                <div class="instruction-list-no-bullet">
                    <li><asp:Label ID="SendStatusMessage" runat="server" Text="StatusMessage" Width="100%"></asp:Label></li>
                </div>                
            </div>             
            <br />
            <div class="instruction-big" style="display:none">Test the PayPal Notification Handler</div>                     
            <div class="instruction" style="display:none">
                 <ul class="instruction-list-no-bullet">
                    <li><asp:Label ID="lblSelfTestInstruction" runat="server" Text="Simulate a PayPal Payment Notification and send a test email to [yourself] in response."></asp:Label></li>
                    <li><asp:Button ID="btnSelfTest" CssClass="NormalButtonVariableSize" runat="server" 
                            Text="Run Test" onclick="btnSelfTest_Click" /></li>
                    <li><asp:Label ID="lblSelfTestResult" runat="server" Text=""></asp:Label></li> 
                 </ul>                
            </div>          
             <div class="instruction-big">Activate the PayPal Notification Handler</div>
             <br />
             <div class="instruction">                  
                Go to <a href='./Registration.aspx?action=2'>Manage Account -> PayPal Settings</a>.
                <br /><br />
                <a href="http://www.simplyfyi.com/Tutorials/TutorialConfigurePayPalIPN/TutorialConfigurePayPalIPN.aspx" target="_blank">Learn how to configure PayPal and activate the PayPal Notification Handler</a>                                                                            
            </div>             
        </div>                                               
    </div>
                                    
    <br />
    <%--<fieldset class="editorsectionfieldset">
        <legend class="editorsectionlegend">Share simplyFYI with friends</legend>
        <div id="fb-root"></div>
        <div id="Div1"></div>
            <script>
                window.fbAsyncInit = function () {
                    FB.init({
                        appId: '169954379709168', // App ID
                        channelURL: '//WWW.simplyfyi.COM/channel.html', // Channel File
                        status: true, // check login status
                        cookie: true, // enable cookies to allow the server to access the session
                        oauth: true, // enable OAuth 2.0
                        xfbml: true  // parse XFBML
                    });

                    // Additional initialization code here
                    // See http://www.1stwebdesigner.com/tutorials/facebook-integrate/

                    var obj = {
                        method: 'feed',
                        redirect_uri: 'http://www.simplyfyi.com',
                        link: 'http://www.simplyfyi.com',
                        picture: 'http://www.simplyfyi.com/img/sms.png',
                        name: 'simplyFYI',
                        caption: 'Some Caption',
                        description: 'using simplyFYI.'
                    };

                    function callback(response) {
                        //document.getElementById('msg').innerHTML = "Post ID: " + response['post_id'];
                    }

                    FB.ui(obj, callback);

                };

                // Load the SDK Asynchronously
                (function (d) {
                    var js, id = 'facebook-jssdk'; if (d.getElementById(id)) { return; }
                    js = d.createElement('script'); js.id = id; js.async = true;
                    js.src = "//connect.facebook.net/en_US/all.js";
                    d.getElementsByTagName('head')[0].appendChild(js);
                } (document));
            </script>


        <script>
            (function (d, s, id) {
                var js, fjs = d.getElementsByTagName(s)[0];
                if (d.getElementById(id)) { return; }
                js = d.createElement(s); js.id = id;
                js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=177784538975255";
                fjs.parentNode.insertBefore(js, fjs);
            } (document, 'script', 'facebook-jssdk'));
        </script>
    
    
        <div class="fb-comments" data-href="http://www.simplyfyi.com" data-num-posts="10" data-width="500"></div>
    
    </fieldset>--%>
    
             
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
