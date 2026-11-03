<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayPalMerchantEmailForwarderSettings.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.PayPalMerchantEmailForwarderSettings" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <%--<asp:Panel runat="server" ID="pnlNewConnectInstructions">
            <div class="instructioncontainer">
            <div class="instruction-big">Connect PayPal to simplyFYI</div>                           
            <br />
            
            </div>
        </asp:Panel>--%>       
        <br />
        <asp:MultiView ID="mvPayPalConnectSettings" runat="server" ActiveViewIndex="0">
            <asp:View ID="SettingsEditView" runat="server" OnActivate="SettingsEditView_Activate">                                                                                      
                <fieldset class="editorsectionfieldset">
                    <legend runat="server" id="legendDescription" class="editorsectionlegend">Last step to complete sign up: Connect PayPal with simplyFYI</legend>
                    <br /><br />
                    <div class="instructioncontainer">
                        <asp:Panel runat="server" ID="pnlNewLinkInstructions">
                            <div class="instruction">
                                <ul class="instruction-list">   
                                    <li>simplyFYI needs to communicate with PayPal to detect when a payment has been made and send your custom confirmation emails.</li>                     
                                    <li>In this step, you will grant permission to simplyFYI to query PayPal on your behalf to look for transactions.</li>                                       
                                    <li>When you click "Continue" below, you will be taken to PayPal where you will login and grant simplyFYI the necessary permissions.</li>
                                    <li>This process is secure and completely managed by PayPal.</li>
				    <li><span style="color:red">Note to Mac users: Please use Firefox or Chrome and NOT Safari to complete this operation.</span> </li>
                                </ul> 
                            </div> 
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlEditLinkInstructions">
                            <div class="instruction">
                                <ul class="instruction-list">
                                    <li>
                                        If your PayPal Primary Email has changed, update it here. Enter the Primary email address on your PayPal account and click "Proceed to PayPal to Grant Permission".
                                    </li>
                                    <li>
                                        If the current PayPal Primary email is correct and you do not have to change it, click "Primary Email is correct".
                                    </li>
                                </ul>                                                        
                            </div>
                        </asp:Panel>                        
                    </div>
                    <table style="padding-top:10px; padding:10px; font-size:14px">
                        <tr>
                            <%--<td style="vertical-align:middle; min-width:36px"><span style="padding-left:10px;padding-right:10px"><asp:Image ID="Image2" runat="server" ImageUrl="~/Images/checkmark.gif" /></span></td>--%>
                            <%-- <td style="vertical-align:middle; min-width:100px">STEP 1: </td>--%>
                            <td style="min-width:350px; padding-top:0px; vertical-align:middle">
                                <div id="div3" runat="server">Enter the Primary email address on your PayPal account:</div>                            
                            </td>
                            <td style="width: 450px; padding-left:10px; vertical-align:middle">
                                <asp:TextBox ID="txtPrimaryEmail" Width="300px" runat="server"></asp:TextBox>
                            </td>  
                        </tr>
                        <tr>
                            <td>
                               
                            </td>
                            <td style="vertical-align:top">
                                <div style="font-size:10px; padding-left:10px">Where is this? Sign in to PayPal. See My Account->Profile->My Business Info->Email (primary)</div>
                                <div style="padding-left:10px"><asp:Label ForeColor="Red" ID="lblMessage" runat="server"></asp:Label> </div>                                           
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="padding-top:25px; padding-left:10px">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button CssClass="NormalButtonVariableSize" ID="btnSetupPayPalConnect" Text="Proceed to PayPal to Grant Permission" runat="server" OnClick="btnSetupPayPalConnect_Click"/>
                                        </td>
                                        <td style="padding-left:20px">
                                            <asp:LinkButton ID="btnCancelEdit" runat="server" Text="Primary email is correct" OnClick="btnCancelEdit_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
<tr>
<td><span style="color:red">Note to Mac users: Please use Firefox or Chrome and NOT Safari to complete this operation.</span> </td>
</tr>
                                </table>
                            </td>
                        
                        </tr>
                    </table>
                    <br />


                </fieldset>
                                 
                                
            </asp:View>
            
            <asp:View ID="SettingsDisplayView" runat="server"> 
                <br />           
            <fieldset class="editorsectionfieldset">
                <legend class="editorsectionlegend">
                    <span runat="server" id="connectStatus">PayPal-simplyFYI connection status</span>
			<%--<img style="padding-left:20px" width="32" src="https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTzIULvjnyiEZg6hH0gmZ-rK0XZDgHHeyV1IEAR0o95ZPhazXR7" alt="connected" width="32"/>--%>
                </legend>
                <br /><br />
                <div class="instructioncontainer">
                    <div class="instruction">
                        <ul class="instruction-list">
                            <li>Your PayPal and simplyFYI accounts are linked. The settings are shown below.</li>
                            <li>If your PayPal Primary Email has changed, update the new Primary Email by clicking "Change primary email".</li>
                        </ul>
                    </div>                    
                </div>
                <br />
                <table width="100%" style="padding-top:10px;font-size:14px">
                    <tr>
                        <td style="padding-bottom:10px; border-bottom:dotted 1px #dadada">
                            <div runat="server" id="divPrimaryEmail">
                                <table>
                                    <tr>
                                        <td style="vertical-align:middle; min-width:36px"><span style="padding-left:10px;padding-right:10px"><asp:Image ID="imgEmailSaved" runat="server" ImageUrl="~/Images/checkmark.gif" /></span></td>
                                        <%-- <td style="vertical-align:middle; min-width:100px">STEP 1: </td>--%>
                                        <td style="min-width:400px; padding-top:0px; vertical-align:middle">
                                            <div id="divForwardingEmailAddress" runat="server">Primary email address on your PayPal account:</div>
                                        </td>
                                        <td style="width: 300px; padding-top:0px; vertical-align:middle">
                                            <asp:Label ID="lblMerchantEmail" Width="300px" runat="server"></asp:Label>                                            
                                        </td> 
                                        <td>
                                            <asp:LinkButton ID="btnEditPrimaryEmail" runat="server" Text="Change primary email" OnClick="btnEditPrimaryEmail_Click" ></asp:LinkButton>
                                        </td> 
                                    </tr>
                                </table>
                            </div>                    
                        </td>                                  
                    </tr> 
                    <tr>  
                        <td style="padding-bottom:10px; padding-top:10px; border-bottom:dotted 0px #dadada">
                            <div runat="server" id="divGrantPermission">
                                <table>
                                    <tr>
                                        <td style="vertical-align:middle; min-width:36px"><span style="padding-left:10px;padding-right:10px"><asp:Image ID="imgPermissionGranted" runat="server" ImageUrl="~/Images/checkmark.gif" /></span></td>
                                        <%-- <td style="vertical-align:middle; min-width:100px">STEP 2: </td>   --%>          
                                        <td style="min-width: 400px; padding-top:0px; vertical-align:middle">
                                            Grant Permission for simplyFYI to read transaction details:                    
                                        </td>
                                        <td style="width: 450px; padding-top:0px; vertical-align:middle">                                          
                                            <div id="divPermissionGrantedImage" runat="server">
                                                <span style="padding-right:10px">Permission granted</span>                        
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>                    
                        </td>                 
                    </tr>                                                 
                </table>
            </fieldset>                               
        </asp:View>         
        </asp:MultiView>                     
    </ContentTemplate>
</asp:UpdatePanel>
