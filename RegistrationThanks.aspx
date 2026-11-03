<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.Master" AutoEventWireup="true" CodeBehind="RegistrationThanks.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.RegistrationThanks" Title="Transactional emails made easy for PayPal merchants" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">

<script type="text/javascript">

    // Oct 9, 2026: This script updates the backend with the CLID of this merchant on registration
    window.addEventListener('load', function () {
        // 1. Fixed session check to match the object being serialized
        var engageUser = <%= Session["EngageUser"] != null ? new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(Session["EngageUser"]) : "null" %>;
    var clid = '<%= Session["CLID"]%>';

    // Ensure engageUser exists before reading properties
    if (!engageUser) {
        console.warn("EngageUser session is null.");
    return;
        }

   
    // 2. Build a clean JavaScript object instead of manual string concatenation
    let payloadData = {
        merchant_clid: clid,
    primary_email: engageUser.Email
        };

    //let url = "https://simplyfyi-pp-onboard.ngrok.io/api/v1/merchant/clid";
    let url = "https://simplyfyi-onboardmanager.azurewebsites.net/api/v1/merchant/clid?code=gSu5z3GpBs-lynDRRtZqVHSlz9xUPfdWvyVATyCJCw2lAzFuXbbJlg==";

    fetch(url, {
        method: 'PUT',
    headers: {
        'Content-Type': 'application/json',              
            },
    // 3. Stringify the object exactly once here
    body: JSON.stringify(payloadData)
        })
        .then(response => response.json())
        .then(data => {
        console.log('API Response:', data);
        })
        .catch(error => {
        console.error('API Error:', error);
        });
    });
</script>





    <div id="maincontent">
        <div class="sectiondivider"> 
            <div id="pagedescriptionbar"> 
                <span><asp:Label ID="PageDescription" Text="Registration successful" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
            </div>
        </div>

        <div class="instructioncontainer">
            <div class="instruction-big">Congratulations! You have successfully registered with simplyFYI.
            <br /><br />
             <%--<div class="instruction-big">Engage with your customers now! Try out our sample Apps.</div>--%>
             Start your Free Trial</div>
             <br />
            <div class="instruction">  
                <table>
                    <tr>
                        <td width="450px" valign="top">
                            We have loaded up your account with 100 simplyFYI UNITS for FREE! We have provided 3 sample FYI apps for you to get started with: <br /><br />
                            <div style="margin-left:40px">
                                <ol>
                                    <li>Sample PayPal Confirmation for Shopping Cart</li>
                                    <li>Sample PayPal Confirmation for Buy Now button</li>
                                    <li>Sample Newsletter</li>
                                </ol>                
                            </div>  
                        </td>
                        <td style="padding-left:30px" valign="top">
                             <img src="https://dl.dropboxusercontent.com/scl/fi/6tx4qwll1ozxmha0c86pg/SampleFYIs.png?rlkey=0y1ky49njpfy13sy9hbf21tix" alt="Samples" />
                        </td>
                    </tr>
                </table>               
                                              
            </div>
            <br />
            <div class="instruction-big">Let's take a Test Drive</div>
            <div class="instruction">  
                <table>
                    <tr>
                        <td width="450px" valign="top">
                            <br />Let's test drive the sample FYI App "Sample PayPal Confirmation for Shopping Cart". When activated, this sample FYI will send a custom payment confirmation email whenever a buyer checks out using the PayPal shopping cart. 
                            In this test drive, we will simulate a checkout which will trigger a customized payment confirmation email (as shown on the right) to be sent to you.
                            <br/><br/>
                            To get started, you need to complete the following 2 steps.<br/><br />
                            <div style="margin-left:40px">
                                <ol>
                              <li>
                                Step 1: Link up your PayPal and simplyFYI accounts. This will get PayPal sending payment notifications to simplyFYI.
                                <a href='http://www.simplyfyi.com/Tutorials/TutorialConfigurePayPalIPN/TutorialConfigurePayPalIPN.aspx' target="_blank">Here's how you do this</a>.
                              </li>

                              <li>
                                Step 2: Activate "Sample Payment Confirmation for Shopping Cart" App to fire when a payment is received. 
                                <a href='http://www.simplyfyi.com/Tutorials/TutorialTestShoppingCartApp/TutorialTestShoppingCartApp.aspx' target="_blank">Here's how you do this</a>.
                              </li>
                            </ol>
                            </div>
                        </td>
                        <td style="padding-left:30px" valign="top">
                            <img src="https://dl.dropboxusercontent.com/scl/fi/8tmx9wr6y7ywdj1vojsju/SampleCartConfirmation-405x367.jpg?rlkey=cr9x5ql6dsk9n3p1ray6ajb82" alt="Samples" />
                        </td>
                    </tr>
                </table>                
                            
                <br /><div class="instruction-big">Customer Support</div><br />
                Enjoy the simplicity and power of simplyFYI. Please do not hesitate to contact us at support@simplyfyi.com for any help. 
                <br />
                <br />
                <asp:Button ID="btnEngageNow" runat="server" Text="Get started!" 
                    onclick="btnEngageNow_Click" />
                <br/>                
            </div> 
                                  
        </div> 
    </div>             
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
