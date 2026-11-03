<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Login" Title="Transactional emails made easy for PayPal merchants" %>

<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">  
<script type="text/javascript">
(function() {
    if (typeof window.janrain !== 'object') window.janrain = {};
    if (typeof window.janrain.settings !== 'object') window.janrain.settings = {};
    
    janrain.settings.tokenUrl = '<%=System.Configuration.ConfigurationManager.AppSettings.Get("JanrainTokenUrl") %>';

    function isReady() { janrain.ready = true; };
    if (document.addEventListener) {
      document.addEventListener("DOMContentLoaded", isReady, false);
    } else {
      window.attachEvent('onload', isReady);
    }

    var e = document.createElement('script');
    e.type = 'text/javascript';
    e.id = 'janrainAuthWidget';

    if (document.location.protocol === 'https:') {
      e.src = 'https://rpxnow.com/js/lib/simplyfyi/engage.js';
    } else {
      e.src = 'http://widget-cdn.rpxnow.com/js/lib/simplyfyi/engage.js';
    }

    var s = document.getElementsByTagName('script')[0];
    s.parentNode.insertBefore(e, s);
})();
</script>    


    <script type="text/javascript">
        $(function () {
            $(document).ready(function () {
                $("#usercredentials").hide();

                $("#teamlogindiv").click(function () {
                    $("#usercredentials").slideToggle(500);
                });
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
                        <div id="janrainEngageEmbed"></div>
                        <div id="teamlogindiv" style="display:none;cursor:pointer; padding-left:0px; margin-top:20px" class="instruction">Team Members click here to login</div>
                        <div id="usercredentials" style="display:none;margin-top:10px; padding-left:20px;padding-top:10px;border:solid 1px #aaaaaa; width:374px">
                            <table>
                                <tr>
                                    <td><span class="instruction">Email</span></td>
                                    <td style="padding-left:15px"><asp:TextBox runat="server" Width="200" ID="txtUsername"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td><span class="instruction">Password</span></td>
                                    <td style="padding-left:15px"><asp:TextBox runat="server" ID="txtPassword" Width="200" TextMode="Password"></asp:TextBox></td>
                                </tr>                                
                                <tr>
                                    <td></td>
                                    <td style="padding-left:15px" align="right">
                                        <asp:Button runat="server" ID="btnSignIn" 
                                            Text="Sign In" onclick="btnSignIn_Click"/></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="instruction"><div style="padding-top:10px; color:Red"><asp:Label runat="server" ID="lblErrorMessage" Width="250"></asp:Label></div></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td  align="left" valign="top" style="padding-top:0px">
                         <div class="instruction-big">EASY AND SECURE SIGN-UP AND SIGN-IN</div>                        
                        <div class="instruction" style="line-height:30px">
                            Log In with PayPal (formerly PayPal Access) is a commerce identity solution provided by PayPal. simplyFYI uses this PayPal service to allow merchants to sign-up for and sign-in to simplyFYI.com quickly and securely using their PayPal login credentials.
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

