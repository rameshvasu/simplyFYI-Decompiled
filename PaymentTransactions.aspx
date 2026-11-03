<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="PaymentTransactions.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.PaymentTransactions" Title="Transactional emails made easy for PayPal merchants" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">   
    <div id="maincontent">   
        <div class="sectiondivider">     
            <div id="pagedescriptionbar"> 
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>            
            </div>
            <%--<span id="pagetitle">View when messages were queued, sent and their status</span>         --%>            
        </div>
        <div class="instructioncontainer">
            <div class="instruction-big">View PayPal payment transactions for a given date range </div>
        </div>
        <br />
        <div id="headernav">
            <ul runat=server id="ListItems">       		    
	            <li runat=server id="Item0" class="on"><a id="Link0" onserverclick="HtmlAnchor_Click" runat=server><span>View Transactions</span></a><span class="navnub"></span></li>       		    
	            <li runat=server id="Item1" ><a id="Link1" onserverclick="HtmlAnchor_Click" runat=server ><span>Import Transaction History</span></a><span class="navnub"></span></li>       		                    
            </ul>        	                
        </div>
        <br />
        <div style="clear:both; position:relative; top:12px;">
        <asp:MultiView runat="server" ID="MultiView1" ActiveViewIndex="0">
            <asp:View runat="server" ID="TransactionsView" >                                       
                <div class="tabsectiondivider">
                   <%-- <div id="reportdaterange" style="vertical-align:middle"></div>--%>
                    <table>
                        <tr>
                            <td><span style="font-size:14px; padding-right:3px; padding-left:3px">Date Range</span></td>
                            <td style="min-width:120px">
                                <asp:DropDownList ID="DDReportPeriod" runat="server">
                                <asp:ListItem Value="1">Today</asp:ListItem>
                                <asp:ListItem Value="7">Last 7 days</asp:ListItem>
                                <asp:ListItem Selected="True" Value="30">Last 30 days</asp:ListItem>
                                <asp:ListItem Value="60">Last 60 days</asp:ListItem>
                                <asp:ListItem Value="90">Last 90 days</asp:ListItem>
                                <asp:ListItem Value="180">Last 180 days</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td><asp:Button CssClass="NormalButton" ID="btnGetReport" runat="server" Text="Show Report" OnClick="btnGetReport_Click" /></td>
                        </tr>
                    </table>
                    <br />                           
                    <div class="editorsectionfieldset">
                        <asp:PlaceHolder ID="PlaceHolderForReport" runat="server"></asp:PlaceHolder>
                    </div>                        
                </div>
            </asp:View> 
            <asp:View runat="server" ID="ImportView">                    
                    <div class="tabsectiondivider">
                        <div class="instructioncontainer">                                                       
                            <div class="instruction">
                                <ul class="instruction-list">
                                    <li>Download Transaction History for your PayPal account as CSV file.<span style="margin-left:5px"><a href="http://www.simplyfyi.com/Tutorials/TutorialDownloadPayPalTransactionHistory/TutorialDownloadPayPalTransactionHistory.aspx" target="_blank">Instructions</a></span></li>
                                    <li>Click "Browse" and select the downloaded .CSV file from your computer.</li>
                                    <li>Click Upload</li>
                                    <li>Click "View Payers"</li>
                                </ul>  
                            </div> 
                        </div>                       
                     
                    <table>
                        <tr>
                            <td style="width:40%; vertical-align:middle">
                                <!--<asp:FileUpload ID="FileUpload1" runat="server" CssClass="NormalButton" Width="251px" />-->
                                <input type=file runat=server id="FileUpload" style="height:30px; width:300px;border: solid 1px #888" />
                            </td>
                            <td style="vertical-align:top;padding-left:15px">                        
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="NormalButton" 
                                    onclick="btnUpload_Click" />                                               
                            </td>  
                                                                                   
                        </tr>
                    </table>
                    <div style="clear:both; margin-left:20px">        
                        <div class="instruction">
                            <div>
                                <asp:Label ID="lblSuccessMessage" runat="server"></asp:Label>
                            </div>
                            <br />
                            <div>
                                    <asp:Label ID="lblExceptionMessage" runat="server"></asp:Label>
                            </div>
                        </div>                                 
                        <br />                   
                        <asp:Panel ID="ExceptionRecordsDisplayPanel" runat="server" Height="290px" Width="1000px" ScrollBars="Auto" HorizontalAlign="Left">
                            <asp:GridView ID="gvUploadedSpreadsheet" EmptyDataText="Spreadsheet has no data." CssClass="GridView" runat="server" GridLines="None" AutoGenerateColumns="False" OnRowDataBound="gvUploadedSpreadsheet_RowDataBound">
                                <SelectedRowStyle BorderStyle="None"/>
                                <HeaderStyle BorderColor="Black" Wrap="False" HorizontalAlign="Left" />
                                <RowStyle Wrap="False" />
                            </asp:GridView>
                            <br />           
                        </asp:Panel>                                
                    </div>
                    </div>                                                    
                </asp:View>          
        </asp:MultiView>
       
        </div>
     </div>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
