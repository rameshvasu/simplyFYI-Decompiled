<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="LoginWithPP.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.LoginWithPP" Title="Transactional emails made easy for PayPal merchants" %>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

<style>
        body {
            font-family: system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
            background: #f5f7fa;
            margin: 0;
            padding: 0;
        }

        .container {
            min-width: 600px;            
            margin: 60px auto;
            margin-top:30px;
            background: #ffffff;
            border-radius: 8px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.06);
            padding: 24px 28px;
        }

        h1 {
            font-size: 1.4rem;
            margin-bottom: 0.75rem;
        }

        p {
            font-size: 0.95rem;
            line-height: 1.5;
            margin: 0 0 0.75rem;
        }

        .trust-note {
            font-size: 0.85rem;
            color: #555;
            margin-top: 0.5rem;
        }

        .paypal-login-button img {
            display: inline-block;
            border: 0;
        }

        .paypal-login-button {
            /*display: inline-block;*/
            margin-top: 1rem;
            cursor: pointer;
        }

        .legal {
            margin-top: 1rem;
            font-size: 0.8rem;
            color: #666;
        }

        .legal a {
            color: #0070ba;
            text-decoration: none;
        }

        .legal a:hover {
            text-decoration: underline;
        }
    </style>

    <div style="text-align:center; margin:0px auto; margin-top:20px" id="pagedescriptionbar">Signup / Login to simplyFYI with PayPal</div>       
<br/>
<div class="container">      
        
          
          <h2>If you are new to simplyFYI:</h2>
<br>

<ul style="padding-left:24px;">
<li>Login with your PayPal Business email. You'll complete PayPal's merchant-onboarding steps, which will link your PayPal account to simplyFYI. During this process, you'll grant simplyFYI permission to read your PayPal transaction details so we can send custom confirmation emails whenever new orders come in. When finished, click "Go back to simplyFYI."</li>
<li>Next in simplyFYI, you will complete a simple registration form and will be ready to start using simplyFYI.</li>
</ul>

<br><h2>If you are returning to simplyFYI:</h2>
<p class="trust-note">Login with your PayPal credentials to access simplyFYI. </p>
        

        

        <!-- Login with PayPal button -->
        <div class="paypal-login-button" onclick="loginWithPayPal()">
            <img src="https://www.paypalobjects.com/devdoc/log-in-with-paypal-button.png"
                 alt="Log in with PayPal" />
        </div>

        <p class="legal">
            By continuing, you agree to our
            <a href="https://www.simplyfyi.com/Privacy.aspx" target="_blank" rel="noopener">Privacy Policy</a>
            and
            <a href="https://www.simplyfyi.com/Terms.aspx" target="_blank" rel="noopener">User Agreement</a>.
        </p>
    </div>

<script>
        function loginWithPayPal() {
            const params = new URLSearchParams({
                client_id: "AVP2ZNYzJKkbJSbKYdhqdiHWGtTj2pmiZJ4I30WWgCTU8o-LBXh3VsdrQO-mKyfn2qyNGmHkEkcmBDq3",
                response_type: "code",
                scope: "openid email profile",
                //redirect_uri: "http://127.0.0.1:7210/api/v1/auth/process"
		redirect_uri: "https://simplyfyi-onboardmanager.azurewebsites.net/api/v1/auth/process"
            });

            const url = "https://www.sandbox.paypal.com/signin/authorize?" + params.toString();
            window.location.href = url;
        }
    </script> 
</asp:Content>