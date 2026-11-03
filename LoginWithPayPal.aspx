<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="LoginWithPayPal.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.LoginWithPayPal" Title="Transactional emails made easy for PayPal merchants" %>

<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">    

<%--Redirect to Login/NewLogin.aspx--%>
<script type="text/javascript">
    window.location.href = "https://www.simplyfyi.com/payments/Login/LoginWithPP.aspx"
</script>  
   
    <script type="text/javascript" src='https://www.paypalobjects.com/js/external/api.js'></script>
    <script type="text/javascript">
    paypal.use(['login'], function (login) {
        login.render({
            "appid": "AdNrNj7z4xXUGyToUbxNvFL6PpaH9GEFQuE0pGFICIqIDiBALR9TeUohWCkEVX5ZrBpVCfUMCCbnGoJy",
            "scopes": "openid email",
            "containerid": "lippButton",
            "responseType": "code",
            "locale": "en-us",
            "buttonType": "LWP",
            "buttonShape": "pill",
            "buttonSize": "lg",
            "fullPage": "true",
            "returnurl": "https://www.simplyfyi.com/payments/LoginWithPayPalAuthNew.aspx"
        });
    });
</script>
    
    <div id="maincontent">   
        <div class="sectiondivider"> 
            <div id="pagedescriptionbar"> 
                <span><asp:Label ID="PageDescription" Text="You need a PayPal Standard, Advanced Or Pro Account to Sign In" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
            </div>            
        </div>
        <br /><br />          
        <div style="text-align:center">
            <%--<h1 style="font-size:2.1em;font-family:Helvetica,Arial,FreeSans,sans-serif;line-height:1em;padding-top:.66em; color:#669900;">Sign Up / Sign In</h1>       --%>
            <br />
            
            <table width="100%">                                
                <tr>                
                    <td align="left" width="450px" style="padding-top:0px" valign="top">
                        <div style="cursor:pointer; padding-left:0px; padding-bottom:10px" class="instruction-big">PayPal Merchant Login</div>                        
                        <div> <span id='lippButton'></span></div>
                        <div class="instruction" style="line-height:30px; padding-right:30px;">
                            If you have trouble signing in, close your browser window (including all open tabs) and try again.
                        </div>
                       
                    </td>
                    <td  align="left" valign="top" style="padding-top:0px">
                         <div class="instruction-big">EASY AND SECURE SIGN-UP AND SIGN-IN</div>                        
                        <div class="instruction" style="line-height:30px">
                            Log In with PayPal is a commerce identity solution provided by PayPal. simplyFYI uses this PayPal service to allow merchants to sign-up for and sign-in to simplyFYI.com quickly and securely using their PayPal login credentials.
                            <br />
                            Log In with PayPal provides an enhanced experience for our customers through:<br />
                            <div style="padding-left:50px">
                                <ul>
                                    <li>Simplicity  - Only one user identity to remember to log in</li>
                                    <li>Peace of mind  - PayPal secures your financial information</li>
                                </ul>                                                                                    
                            </div>                            
                        </div>
                        <br />
                        <div style="line-height:30px">
                            <div class="instruction-big">BROWSERS SUPPORTED</div>
                      
                            <div class="instruction">
                            At this time, simplyFYI.com supports the following browsers:<br /> 
                                <div style="padding-left:50px">
                                    <ul>
                                        <li>Mozilla® Firefox®, most recent stable version (recommended)</li>
                                        <li>Google Chrome™, most recent stable version</li>
                                        <li>Microsoft® Internet Explorer® versions 7, 8, 9, 10, 11</li>
                                        <li><span style="color:red">Note to Mac users: Please use Firefox or Chrome and NOT Safari</span> </li>
                                    </ul> 
                                </div>
                                                                       
                            </div>
                        </div>
                    </td>                    
                </tr>
                <tr>
                    <td align="left">
                        
            
                    </td>
                </tr>
                </table>
            <br /><br />
            <%--<div class="instruction-big">New to simplyFYI? <a href="SignUp.aspx">Join now.</a></div>--%>
        </div>     
        
    </div>        
</asp:Content>

