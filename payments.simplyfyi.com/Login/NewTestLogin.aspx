<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="NewTestLogin.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.NewTestLogin" Title="Transactional emails made easy for PayPal merchants" %>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div id="pagedescriptionbar">Login to simplyFYI</div>
       
<br/>
 <!-- Log in with PayPal (clean, compliant snippet) -->
<div class="paypal-login">
  <p class="paypal-login-text">
    Log in securely with PayPal. You’ll be redirected to PayPal to authenticate, and your PayPal password is never shared with us.
  </p>

<asp:Button ID="btnPPLogin" runat="server" Text="Log in with PayPal" OnClick="btnPPLogin_Click" />


  <a style="display:none" id="btnPPLogin"
    href=""
    class="paypal-login-button"
  >
    <img
      src="https://www.paypalobjects.com/devdoc/log-in-with-paypal-button.png"
      alt="Log in with PayPal"
    />
  </a>

  <p class="paypal-privacy">
    By continuing, you agree to our
    <a href="/privacy-policy" target="_blank" rel="noopener">Privacy Policy</a>
    and
    <a href="/user-agreement" target="_blank" rel="noopener">User Agreement</a>.
  </p>
</div>

<style>
  .paypal-login {
    max-width: 420px;
    margin: 1.5rem 0;
    font-family: system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
    color: #222;
  }

  .paypal-login-text {
    margin-bottom: 0.75rem;
    font-size: 0.95rem;
    line-height: 1.4;
  }

  .paypal-login-button img {
    display: inline-block;
    border: 0;
  }

  .paypal-privacy {
    margin-top: 0.75rem;
    font-size: 0.8rem;
    color: #555;
  }

  .paypal-privacy a {
    color: #0070ba;
    text-decoration: none;
  }

  .paypal-privacy a:hover {
    text-decoration: underline;
  }
</style>

<script>
  const ONBOARD_URL = "https://simplyfyi-paypal-platform.azurewebsites.net/api/v1/partner/referral";

  document.getElementById("btnPPLogin").onclick = async () => {
    const res = await fetch(ONBOARD_URL, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "x-functions-key": "YGriM8crZFxMNiatwA4MZzQ9BXl2AQOST59XJ721AAvcAzFu3hMkFw=="
      },
      body: JSON.stringify({})
    });

    const data = await res.json();
    console.log(data);

    window.location.href = data.redirectUrl;
  };
</script>


</asp:Content>