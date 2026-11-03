<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GetTransactionsCapabilitySettings.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.GetTransactionsCapabilitySettings" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:MultiView ID="mvGetTransactionsCapabilitySettings" runat="server" ActiveViewIndex="0">            

            <asp:View ID="GetTransactionsCapabilityView" runat="server">
                <br />
                <fieldset class="editorsectionfieldset">
                    <legend id="get-transactions-capability-status" class="editorsectionlegend">
                        <span runat="server" id="linkedStatusTitle">PayPal is connected to simplyFYI</span>
			<%--<img style="padding-left:20px" width="32" src="https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTzIULvjnyiEZg6hH0gmZ-rK0XZDgHHeyV1IEAR0o95ZPhazXR7" alt="connected" width="32" />--%>
                    </legend>
                    <br /><br />
                    <div class="instructioncontainer">
                        <div class="instruction">
                            <ul id="get-transactions-capability-instructions" class="instruction-list">
                                <li><span runat="server" id="linkedStatus">simplyFYI is LINKED with PayPal</span> to read your order transactions.</li>
                                <li>You can enable or disable this capabilty by toggling the switch below.</li>
                            </ul>                            
                        </div>
                    </div>
                    <br />

                    <table width="100%" style="padding-top:10px;font-size:14px">
                        <tr>
                            <td style="padding-bottom:10px; border-bottom:dotted 1px #dadada">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 600px; padding-top:5px">
                                                Read order transactions from PayPal?
                                            </td>
                                            <td style="width: 400px; padding-top:15px">
                                                <div>						    			
                                                    <%--<input id="cbGetTransactions" runat="server" type="checkbox" name="cbGetTransactions" onclick=""><span style="padding-left:10px"><span class="instruction">Yes / No</span></span>--%>
						    <input id="cbGetTransactions" type="checkbox" runat="server" onserverchange="cbGetTransactions_ServerChange" 
onchange="__doPostBack(this.name, '');" /><span style="padding-left:10px"><span class="instruction">Yes / No</span></span>
                                                </div>
                                            </td>
                                        </tr>
					<tr>
					    	<td style="width: 600px; padding-top:5px"><asp:Label ID="lblResult" runat="server">SOFT LINK</asp:Label></td>
					</tr>
                                        
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        
                    </table>
                </fieldset>
            </asp:View>
        </asp:MultiView>
    </ContentTemplate>
</asp:UpdatePanel>
