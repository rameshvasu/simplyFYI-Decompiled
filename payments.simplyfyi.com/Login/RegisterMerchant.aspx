<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="RegisterMerchant.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.RegisterMerchant" Title="Transactional emails made easy for PayPal merchants" %>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div style="width:100%;text-align:center; padding-top:20px" id="pagedescriptionbar">Yay! simplyFYI is now connected to your PayPal business account.</div>

    <div class="paypal-login">
        <p class="paypal-login-text">
            <h2 style="color:rgb(0, 175, 227)">simplyFYI-PayPal Connection Summary</h2>
            <br>

            <div style="text-align:center">
                <table class="sfy-consent-table">
  <thead>
    <tr>
      <th style="text-align:left;width:50%; padding:10px;">What simplyFYI CAN do</th>
      <th style="text-align:left;width:50%; padding:10px;">What simplyFYI CANNOT do</th>
    </tr>
  </thead>
  <tbody style="text-align:left">
    <tr valign="top">
      <td style="padding:10px;">
        <strong>Access your business profile</strong><br />
        Read your PayPal business name, email, and merchant ID to connect your account correctly.
      </td>
      <td style="padding:10px;">
        <strong>Charge your customers</strong><br />
        simplyFYI cannot initiate or modify customer payments.
      </td>
    </tr>

    <tr valign="top">
      <td style="padding:10px;">
        <strong>Access your transaction history</strong><br />
        Read your PayPal transactions to send branded payment‑confirmation emails and help you track activity.
      </td>
      <td style="padding:10px;">
        <strong>Deduct money from your PayPal sales</strong><br />
        simplyFYI cannot withdraw or remove funds from any transaction.
      </td>
    </tr>

    <tr valign="top">
      <td style="padding:10px;">
        <strong>Calculate platform fees (Partner Fee)</strong><br />
        Compute usage‑based fees for your simplyFYI plan.<br />
        <em>No fees are ever deducted from your PayPal account.</em>
      </td>
      <td style="padding:10px;">
        <strong>Issue refunds</strong><br />
        simplyFYI cannot process refunds on your behalf.
      </td>
    </tr>

    <tr valign="top">
      <td style="padding:10px;">
        <strong>Manage your simplyFYI subscription</strong><br />
        Allow PayPal to process subscription payments if you enable auto‑replenish.
      </td>
      <td style="padding:10px;">
        <strong>Access or move your PayPal balance</strong><br />
        simplyFYI cannot view, transfer, or manipulate your PayPal funds.
      </td>
    </tr>

    <tr valign="top">
      <td style="padding:10px;">
        <strong>Maintain your connection status</strong><br />
        Show whether your PayPal account is connected, pending, or needs attention.
      </td>
      <td style="padding:10px;">
        <strong>Modify, capture, or authorize payments</strong><br />
        simplyFYI cannot perform any payment‑control actions.
      </td>
    </tr>
 <tr>

            <td colspan="2" style="text-align:center;border-top:1px solid #dadada; padding-top:20px;"><asp:Button ID="btnRegisterMerchant" runat="server" CssClass="VariableWidthButton" Text="Let's get started with using simplyFYI" OnClick="btnRegisterMerchant_Click" /></td>
        </tr>

  </tbody>
</table>

            </div>




            <style>
                .paypal-login {
                    text-align: center;
                    width: 100%;
                    margin: 1.5rem 0;
                    font-family: system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
                    color: #222;
                    margin-top: 0px;
                }

                .paypal-login-button {
                    cursor: pointer;
                }

                .logo-tagline {
                    text-align: center;
                    width: 100%;
                    margin: 1.5rem 0;
                    font-family: system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
                    color: #222;
                    margin-top: 100px;
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
      //const ONBOARD_URL = "http://localhost:7210/api/v1/partner/referral";
      const ONBOARD_URL = "https://simplyfyi-paypal-platform.azurewebsites.net/api/v1/partner/referral?code=YGriM8crZFxMNiatwA4MZzQ9BXl2AQOST59XJ721AAvcAzFu3hMkFw==";

    document.getElementById("onboardBtn").onclick = async (e) => {
    e.preventDefault();

    // Show status message
    document.getElementById("authStatus").innerText = "Authenticating with PayPal…";

    const res = await fetch(ONBOARD_URL, { method: "POST" });
    const data = await res.json();

    // Redirect merchant to PayPal onboarding
    window.location.href = data.redirectUrl;
};


            </script>
            </body>

            <br />

</asp:Content>