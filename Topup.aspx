<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Topup.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Topup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script>
   <%-- Sep 29, 2026: Immediately redirect to new payment flow page--%>
   window.location.replace("Topup.htm");
</script>


<head runat="server">
    <title>Topup</title>
    <link type="text/css" href="app_themes/White/Styles.css"  />    
    <style type="text/css">
        .auto-style1 {
            height: 145px;
            vertical-align:top;
        }
    </style>
</head>

<body>	
     <div style="padding-top:0px; padding-bottom:0px;padding-left:0px; border-bottom:solid 0px #dadada; width:100%; position:relative;top:-2px;">
      <table width=100%>
                <tr>
                    <td valign=top align=center>
                        <div id="mainheader">
	    <div style="padding-top:1px; padding-bottom:5px; border-bottom:solid 0px #dadada; width:100%;">
                        <table width="100%">
                            <tr>
                                <td width=200px align=left>
                                    <span id="logopart1">                            
                                    simply</span><span id="logopart2">FYI</span>
                                    <span id="beta">beta</span>
                                    <span style="font-size:small; color:#CC6600; font-weight:bold"></span>
                                </td>
                                <td valign="middle" align="center">
                                    <span id="tagline">
                                        <span runat="server" id="taglinecontent">where content becomes communication</span>                           
                                    </span>     
                                </td>
                                <td valign="middle" style="width:200px">
                                    <div id="dropdownmenu" style="float:right;margin-left:2px; margin-right:55px; padding-top:0px; display:none;">
                                        <ul>        
                                            <li><a href="#"><asp:Label ID="lblWelcomeUser" runat="server" Text="Welcome user"></asp:Label><img style="padding-left:5px; background-color:transparent; border-style:none" src="images/dropdown-arrow.jpg" width="10px" alt="" /></a>
                                                <ul>
                                                    <li><a href="./EngageNow.aspx">Engage Now!</a></li>
                                                    <li><a href="./SelectChannel.aspx">Create a FYI</a></li>
                                                    <li><a href="./PickFYI.aspx">Manage FYIs</a></li>
                                                    <li><a href="./MessageStatus.aspx">Analytics</a></li>
                                                    <li><a href="./ManageQueuedMessages.aspx">Queue</a></li>
                                                    <li><a href="./Registration.aspx?action=2">Account</a></li>
                                                    <li><a href="./Logout2.aspx">Logout</a></li>
                                                </ul>
                                            </li>       
                                        </ul>
                                    </div> 
                                </td>
                            </tr>
                            <tr>                                
                                <td>  
                                    <span id="logotagline">easy customer engagement</span>                                  
                                </td>
                                <td>
                                    
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>                     
    </div>          
     <div style="position:relative; top:-55px; left:-8px">                                 
	<div id="mainwrapper">	
    <div id="copy">
    <div id="maindivwide">         
        <div id="maincontent">
          
                <div class="sectiondivider"> 
                <div style="position:relative;left:15px;top:4px">
                    <div id="pagedescriptionbar"> 
                   <%-- <span><asp:Label ID="PageDescription" Font-Size="14px" Text="Purchase Units" runat="server" /></span>--%>   
                   Purchase Units                                                                                                                                                                                                                                                                                                
                </div>
                </div>
                
            </div>
            <div class="instructioncontainer">
                <div class="instruction-big">
                     Topup your simplyFYI account with extra units using secure e-commerce by PayPal.
                </div>
            </div>
            <br />
            <table>
                <tr>
                    <td style="padding-right:5px; border-right:dotted 1px #888; width:350px; vertical-align:top">
                        <table>
                            <tr>
                                <td class="auto-style1">
                                    <div style="padding-left:30px">
                                        <%--<form action="https://www.paypal.com/cgi-bin/webscr" method="post" target="_blank">--%>
                                        <form action="https://www.paypal.com/cgi-bin/webscr" method="post" target="_self">

                                            <input type="hidden" name="cmd" value="_s-xclick">
                                            <input type="hidden" name="hosted_button_id" value="D4L9P4K3G5HB2">
                                            <table>
                                                <tr>
                                                    <td style="vertical-align:top"><input type="hidden" name="on0" value="simplyfy Units">
                                                        <span style="color: #3C7FAF;font-family:Arial;font-weight:bold;	font-size: 1.2em;">
                                                            One-time purchase of simplyFYI UNITS
                                                        </span>
                                                        <span class="instruction"><br />Refill your balance with a one-time payment.</span>
                                                    </td>                                                    
                                                </tr>
                                                <tr>
                                                    <td style="padding-top:10px">
                                                        <select name="os0">	                                    
	                                                        <option value="1000 units">1000 units $10.00 USD</option>
	                                                        <option value="2000 units">2000 units $20.00 USD</option>
	                                                        <option value="3000 units">3000 units $30.00 USD</option>
	                                                        <option value="5000 units">5000 units $50.00 USD</option>
	                                                        <option value="7500 units">7500 units $75.00 USD</option>
	                                                        <option value="10000 units">10000 units $100.00 USD</option>
                                                        </select>
                                                    </td>
                                                </tr>
                                            </table>
                                            <input type="hidden" name="custom" value="<%=CustomValue %>">
                                            <input type="hidden" name="return" value="<%=PaymentCompletedReturnUrl %>">
                                            <input type="hidden" name="cancel_return" value="<%=PaymentCanceledReturnUrl %>">
                                            <input type="hidden" name="currency_code" value="USD">                        
                                            <input type="hidden" name="notify_url" value="http://www.simplyfyiapps.com/paypalipn/paypalipn.aspx">
                                            <input type="image" src="https://www.paypal.com/en_US/i/btn/btn_buynowCC_LG.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!">
                                            <img alt="" border="0" src="https://www.paypal.com/en_US/i/scr/pixel.gif" width="1" height="1">
                                        </form>                                        
                                    </div>
                                </td>                                   
                            </tr>
                            <tr>
                                <td valign="top">
                                    <!-- PayPal Logo -->
                                    <table border="0" cellpadding="5" cellspacing="0" align="center">                    
                                        <tr>
                                            <td align="center" style="height: 103px">
                                                <a href="#" onclick="javascript:window.open('https://www.paypal.com/us/cgi-bin/webscr?cmd=xpt/Marketing/popup/OLCWhatIsPayPal-outside','olcwhatispaypal','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=400, height=350');">
                                                    <img  src="https://www.paypal.com/en_US/i/bnr/horizontal_solution_PPeCheck.gif" border="0" alt="Solution Graphics">
                                                </a>
                                            </td>
                                        </tr>                                        
                                    </table>
                                    <!-- PayPal Logo -->
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding-left:15px; vertical-align:top; padding-top:0px">
                        <table>
                            <tr>                                  
                                <td>
                                    <div id="divNewBillingAgreement" runat="server">
                                        <span style="color: #3C7FAF;font-family:Arial;font-weight:bold;	font-size: 1.2em;">Automated Purchase of simplyFYI UNITS<span class="newfeature">NEW FEATURE</span></span>
                                            <div style="padding-top:10px">
                                                <span class="instruction">
                                                    You can now automate future purchase of simplyFYI UNITS by signing a Billing Agreement with PayPal. Sign up for automated billing and make sure that your customers are always receiving your messages.
                                                    <br /><br />
                                                    <u>How It Works:</u> Whenever your simplyFYI UNITS balance goes below 300 UNITS, we will automatically purchase additional UNITS on your behalf and bill your PayPal account. You choose the quantity of additional UNITS that you want purchased automatically.                                                                                                
                                                </span>
                                                
                                                <div style="padding-top:10px">
                                                        <fieldset style="padding-top:10px; text-align:center">                                                
                                                            <div>
                                                                <span class="instruction">UNITS to purchase automatically when balance goes below 300 UNITS:
                                                                    <select id="ddBillAmount" name="BillAmount">                                                
                                                                        <option value="10.00" selected="selected">1000 UNITS $10.00 USD</option>
                                                                        <option value="20.00">2000 UNITS $20.00 USD</option>
                                                                        <option value="30.00">3000 UNITS $30.00 USD</option>
                                                                        <option value="50.00">5000 UNITS $50.00 USD</option>
                                                                        <option value="75.00">7500 UNITS $75.00 USD</option>
                                                                        <option value="100.00">10000 UNITS $100.00 USD</option>
                                                                    </select>
                                                                </span>
                                                            </div>
                                                
                                                            <div>
                                                                <input class="NormalButtonVariableSize" type="submit" id="btnBillingAgreement" value="Create Billing Agreement with PayPal" onclick="disableBillingAgreementButton();" /> 
                                                            </div>
                                                        </fieldset>
                                                    </div>                                                
                                                                                                                                                                                                               
                                            </div>
                                    </div>
                                    <div id="divExisitingBillingAgreement" runat="server">
                                        <span style="color: #3C7FAF;font-family:Arial;font-weight:bold;	font-size: 1.2em;">Automated Purchase of simplyFYI UNITS<span class="newfeature">NEW FEATURE</span></span>
                                            <div style="padding-top:10px">
                                                <span class="instruction" id="spanExistingAutomatedBillingDetails" runat="server">                                                                                                       
                                                </span>
                                            </div>                                        
                                    </div>                                    
                                </td>                                
                            </tr>
                        </table>
                    </td>
                </tr> 
                <tr>
                    <td style="padding-top:10px; text-align:right" colspan="2"><a id=BackLink runat=server href="./Start.aspx">Back to Control Panel</a></td>
                </tr>                 
            </table>            
            
            </div>
          
        
    </div>           
    </div>  
    </div>    
    </div>
                    </td>
                </tr>
          <tr align=center>
                    <td>
                        <div style="padding-top:20px;margin-top:25px;clear:both;border-top:solid 0px #eeeeee; width:100%; bottom:15px;position:relative;margin-left:0px;">
                            <div id="footertext">Copyright 2014 &copy; simplyFYI Communications. All rights reserved.</div>  
                        </div>                                                                                   
                    </td>
                </tr> 
        </table>
	
       </div>                  

    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script type="text/javascript">
        function disableBillingAgreementButton() {            
            btnBillingAgreement.value = "Contacting PayPal...";
            btnBillingAgreement.setAttribute('disabled', 'disabled');
            //$.get("./TopupAuto.aspx?BillAmount=" + $("#ddBillAmount").val());
            window.location.replace("./TopupAuto.aspx?BillAmount=" + $("#ddBillAmount").val());
    }
</script>
</body>
</html>
