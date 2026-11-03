<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="ConnectPayPal.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.ConnectPayPal" Title="Transactional emails made easy for PayPal merchants" %>

<%@ Register src="Controls/ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="headEmbeds" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>

<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">  
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>           
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>  
             <asp:Panel runat="server" ID="pnlSuccess" Visible="true">  
                <div class="instructioncontainer">                               
                    <div class="instruction-big"><span style="padding-right:5px"><img src="images/checkmark.gif" alt="check" /></span>Congratulations! You have successfully registered with simplyFYI!</div>
                </div>
                <br />
                           
                 <table>
                     <tr>
                         <td>
                             <div class="instruction-big-orange">Start your Free Trial</div>
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
                             <img src="https://dl.dropboxusercontent.com/u/24992047/simplyFYI/Tutorials/QuickStart/SampleFYIs.png" alt="Samples" />
                        </td>
                    </tr>
                </table>               
                                              
            </div>
            <br />
            <div class="instruction-big-orange">Take a Test Drive</div>
            <div class="instruction">  
                <table>
                    <tr>
                        <td width="450px" valign="top">
                            <br />Let's test drive the sample FYI App "Sample PayPal Confirmation for Shopping Cart". This sample FYI will send a custom payment confirmation email whenever a buyer checks out using the PayPal shopping cart. 
                            In this test drive, we will simulate a checkout which will trigger a customized payment confirmation email (as shown on the right) to be sent to you.
                            <br/><br/>
                            Send a &quot;Sample PayPal Confirmation for Shopping Cart&quot; test email now!<br/><br />
                            <div style="margin-left:40px">
                                <ol>
                                    <li>
                                        Click &quot;Get Started&quot; button below. You will be taken to the &quot;Contol Panel&quot; page.
                                    </li>
                                    <li>
                                        From the &quot;Control Panel&quot;, click &quot;Manage FYIs&quot;. 
                                    </li>
                                    <li>
                                        Open &quot;Sample PayPal Confirmation for Shopping Cart&quot; by clicking its &quot;Open&quot; link. This will open the Editor.
                                    </li>
                                    <li>
                                        In the Editor, click &quot;Preview&quot; and then click &quot;Send Test Email&quot;.
                                    </li>                                    
                                </ol>
                            </div>
                        </td>
                        <td style="padding-left:30px" valign="top">
                            <img src="https://dl.dropboxusercontent.com/u/24992047/simplyFYI/Tutorials/QuickStart/SampleCartConfirmation%20%28405x367%29.jpg" alt="Samples" />
                        </td>
                    </tr>
                </table>                
                            
               <%-- <br /><div class="instruction-big-orange">Customer Support</div><br />
                Enjoy the simplicity and power of simplyFYI. Please do not hesitate to contact us at support@simplyfyi.com for any help. 
                <br />
                <br />--%>
                <asp:Button ID="btnEngageNow" runat="server" Text="Get started!" 
                    onclick="btnEngageNow_Click" />
                <br/>                
            </div> 
                                  
        </div>    

                         </td>
                     </tr>
                 </table>            
                 <%--<table>
                     <tr>
                         <td style="vertical-align:top; padding-right:10px">
                            <div class="instruction"><br />The link between your PayPal and simplyFYI accounts has been established successfully.
                                <br /><br />When PayPal receives a payment, simplyFYI will be notified and will in trigger the appropriate FYI to send custom confirmation emails.
                                <br /><br />If you haven't done so already, you need to activate activate a FYI before your customers start receiving your custom confirmation emails.
                                <a href="http://www.simplyfyi.com/Tutorials/TutorialIndex.aspx" target="_blank"> Learn more here.</a> 
                            </div>
                         </td>
                         <td style="vertical-align:top">                             
                             <div style="border:solid 1px #dadada; padding:5px">
                                <img src="http://www.simplyfyi.com/img/howitworks4.png" alt="simplyfyi" width="600" />
                             </div>
                         </td>
                     </tr>
                 </table>--%>                                                
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlError" Visible="false">                
                <div class="instructioncontainer">                               
                    <div class="instruction-big"><span style="padding-right:5px"><img width="20" src="images/failed.png" alt="check" /></span>Unable to establish PayPal-simplyFYI connection</div>
                </div>
                <br />                
                <br /><div class="instruction">An error was encountered in establishing the connection between your PayPal and simplyFYI accounts. 
                <br /><br />Confirm the PayPal Primary Email Address and click "Proceed to PayPal to Grant Permission" again.</div>                                                         
                <br /><br />                
                
                 <fieldset class="editorsectionfieldset">
                    <legend runat="server" id="legendDescription" class="editorsectionlegend">Let's try this again</legend>
                        <br /><br />
                        <table style="padding-top:10px; font-size:14px">
                            <tr>
                                <%--<td style="vertical-align:middle; min-width:36px"><span style="padding-left:10px;padding-right:10px"><asp:Image ID="Image2" runat="server" ImageUrl="/Images/checkmark.gif" /></span></td>--%>
                                <%-- <td style="vertical-align:middle; min-width:100px">STEP 1: </td>--%>
                                <td style="min-width:450px; padding-top:0px; vertical-align:middle">
                                    <div id="div3" runat="server">Enter the Primary email address on your PayPal account:</div>                            
                                </td>
                                <td style="width: 450px; padding-top:0px; vertical-align:middle">
                                    <asp:TextBox ID="txtPrimaryEmail" Width="300px" runat="server"></asp:TextBox>                                            
                                </td>  
                            </tr>
                            <tr>
                                <td>
                            
                                </td>
                                <td style="vertical-align:top">
                                    <div style="font-size:10px;">Where is this? Sign in to PayPal. See My Account->Profile->My Business Info->Email (primary)</div>
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="padding-top:25px">
                                    <asp:Button CssClass="NormalButtonVariableSize" ID="btnSetupPayPalConnect" Text="Proceed to PayPal to Grant Permission" runat="server" OnClick="btnSetupPayPalConnect_Click"/>
                                </td>
                        
                            </tr>
                        </table>
                </fieldset>
            </asp:Panel>
                                        
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
