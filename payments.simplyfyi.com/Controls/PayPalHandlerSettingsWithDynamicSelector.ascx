<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayPalHandlerSettingsWithDynamicSelector.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.PayPalHandlerSettingsWithDynamicSelector" %>
<%@ Register src="ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc1" %>

<%@ Register src="PayPalSingleItemFYISelector.ascx" tagname="PayPalSingleItemFYISelector" tagprefix="uc2" %>

<%--See http://jquerybyexample.blogspot.com/2012/02/how-to-use-jquery-settimeout-function.html--%>

<script type="text/javascript">   
    $(function () {        
        $(document).ready(function () {
            $("#<%=btnRunTest.ClientID %>").bind('click', function () {
                setTimeout(function () {
                    $("#<%=lblInfo.ClientID %>").hide(1000);
                }, 10000);
            });                                          
        })
    });
</script>
 
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <table width="100%">                                                                                                              
            <tr>
                <td style="width: 521px; padding-top:5px; padding-bottom:5px">
                    <fieldset style="height:auto">
                        <legend style="font-size:16px">Select FYI to run</legend> 
                        <asp:DropDownList ID="DDNotificationHandlers" runat="server" Width="466px" 
                        onselectedindexchanged="DDNotificationHandlers_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>  
                        <br /><br />
                        <div id="SingleItemFYISelectorDIV" runat="server" style="display:none">
                            <div style="font-size:16px; color:#F63">OPTIONAL: Run a FYI based on item name or item number <span class="newfeature">NEW FEATURE</span></div> 
                            <uc2:PayPalSingleItemFYISelector ID="PayPalSingleItemFYISelector1" runat="server" />                                                                         
                        </div>                       
                    </fieldset> <br />                                                              
                </td>                    
            </tr>                                                              
            <tr style="display:none">
                <td>
                    <fieldset>
                        <legend style="font-size:16px">Test With Simulated Payment Data</legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnRunTest" CssClass="NormalButtonVariableSize" runat="server" Text="Test the Notification Handler" onclick="btnRunTest_Click" />     
                                    <br />   
                                    <asp:Label ID="lblInfo" runat="server" Text="Label"></asp:Label> 
                                </td>
                                <td>
                                    <span style="padding-left:10px; padding-right:10px; color:#888"> using transaction </span>
                                    <asp:DropDownList ID="DDSampleTransactions" runat="server"></asp:DropDownList>                                    
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <fieldset style="padding-bottom:10px">
                        <legend style="font-size:16px">Working with Live Production Data</legend>
                        <table>                            
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="width: 600px; padding-top:5px">
                                                Process live payments from PayPal? </td>
                                            <td style="width: 400px; padding-top:15px">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="cbActivateHandler" runat="server" AutoPostBack="true" 
                                                        oncheckedchanged="cbActivateHandler_CheckedChanged" /><span style="padding-left:10px"><span class="instruction">Yes / No</span></span>    
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>                                                                   
                                            </td>  
                                        </tr>
                                        <tr>
                                            <td style="width: 600px; padding-top:5px">
                                               Select message flow mode </td>
                                            <td style="width: 400px; padding-top:15px">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="DDMessageFlowMode" runat="server" Width="360px" 
                                                         AutoPostBack="true" 
                                                            onselectedindexchanged="DDMessageFlowMode_SelectedIndexChanged">                                                            
                                                         </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>                                                                   
                                            </td>  
                                        </tr>
                                    </table>  
                                </td>
                            </tr>                         
                        </table>
                    </fieldset>                
                </td>                                  
            </tr>
        </table>        
    </ContentTemplate>
</asp:UpdatePanel>
    
<asp:HiddenField runat="server" ID="_TransactionTypeHiddenField" />    
<asp:HiddenField runat="server" ID="_CLIDHiddenField" />             
<uc1:ModalDialog ID="ModalDialog1" runat="server" />
             
