<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="MessageStatus.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.MessageStatus" Title="Transactional emails made easy for PayPal merchants" %>
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
            <div class="instruction-big">View when messages were sent and who opened them</div>
        </div>
        <br />
        <div id="headernav">
            <ul runat=server id="ListItems">  
                <li runat=server id="Item3" class="on"><a id="Link3" onserverclick="HtmlAnchor_Click" runat=server ><span>Dashboard</span></a><span class="navnub"></span></li>     		    
	            <li runat=server id="Item0"><a id="Link0" onserverclick="HtmlAnchor_Click" runat=server><span>Email Delivery Details</span></a><span class="navnub"></span></li>       		    
	            <li runat=server id="Item1" ><a id="Link1" onserverclick="HtmlAnchor_Click" runat=server ><span>Email Opens Details</span></a><span class="navnub"></span></li>       		    
                <li runat=server id="Item2" ><a id="Link2" onserverclick="HtmlAnchor_Click" runat=server ><span>Email Bounces Details</span></a><span class="navnub"></span></li>                                
            </ul>        	                
        </div>
        
        <div style="clear:both; position:relative; top:12px;">
        <asp:MultiView runat="server" ID="MultiView1" ActiveViewIndex="3">
            <asp:View runat="server" ID="View1" >                                       
                <div class="tabsectiondivider">
                   <%-- <div id="reportdaterange" style="vertical-align:middle"></div>--%>
                    <table>
                        <tr>
                            <td><span style="font-size:14px; padding-right:3px">Messages queued in </span></td>
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
            <asp:View ID="View2" runat="server">   
                <div class="tabsectiondivider">                        
                    <table>
                        <tr>
                            <td><span style="font-size:14px; padding-right:3px">Messages queued in </span></td>
                            <td style="min-width:120px">
                                <asp:DropDownList ID="DDOpensReportPeriod" runat="server">
                                <asp:ListItem Value="1">Today</asp:ListItem>
                                <asp:ListItem Value="7">Last 7 days</asp:ListItem>
                                <asp:ListItem Selected="True" Value="30">Last 30 days</asp:ListItem>
                                <asp:ListItem Value="60">Last 60 days</asp:ListItem>
                                <asp:ListItem Value="90">Last 90 days</asp:ListItem>
                                <asp:ListItem Value="180">Last 180 days</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td><asp:Button CssClass="NormalButton" ID="btnGetEmailOpensReport" runat="server" 
                                Text="Show Report" OnClick="btnGetEmailOpensReport_Click" /></td>
                        </tr>
                    </table>
                    <br />                           
                    <div class="editorsectionfieldset">
                        <asp:PlaceHolder ID="PlaceHolderForEmailOpensReport" runat="server"></asp:PlaceHolder>
                    </div> 
                </div>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div class="tabsectiondivider">
                    <table>
                        <tr>
                            <td><span style="font-size:14px; padding-right:3px">Messages queued in </span></td>
                            <td style="min-width:120px">
                            <asp:DropDownList ID="DDBouncesReportPeriod" runat="server">
                                <asp:ListItem Value="1">Today</asp:ListItem>
                                <asp:ListItem Value="7">Last 7 days</asp:ListItem>
                                <asp:ListItem Selected="True" Value="30">Last 30 days</asp:ListItem>
                                <asp:ListItem Value="60">Last 60 days</asp:ListItem>
                                <asp:ListItem Value="90">Last 90 days</asp:ListItem>
                                <asp:ListItem Value="180">Last 180 days</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td><asp:Button CssClass="NormalButton" ID="btnGetEmailBouncesReport" 
                                runat="server" Text="Show Report" OnClick="btnGetEmailBouncesReport_Click" /></td>
                        </tr>
                    </table>
                    <br />                           
                    <div class="editorsectionfieldset">
                        <asp:PlaceHolder ID="PlaceHolderForEmailBouncesReport" runat="server"></asp:PlaceHolder>
                    </div> 
                </div>
            </asp:View>
            <asp:View ID="View4" runat="server">
                <div class="tabsectiondivider">
                    <table>
                        <tr>
                            <td><span style="font-size:14px; padding-right:3px">Messages queued in </span></td>
                            <td style="min-width:120px">
                                <asp:DropDownList ID="DDDashboardReportPeriod" runat="server">
                                <asp:ListItem Value="1">Today</asp:ListItem>
                                <asp:ListItem Value="7">Last 7 days</asp:ListItem>
                                <asp:ListItem Selected="True" Value="30">Last 30 days</asp:ListItem>
                                <asp:ListItem Value="60">Last 60 days</asp:ListItem>
                                <asp:ListItem Value="90">Last 90 days</asp:ListItem>
                                <asp:ListItem Value="180">Last 180 days</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td><asp:Button CssClass="NormalButton" ID="btnGetDashboardReport" 
                                runat="server" Text="Show Report" 
                                onclick="btnGetDashboardReport_Click" /></td>
                        </tr>
                    </table>
                    <br />                           
                    <div class="editorsectionfieldset">
                        <asp:PlaceHolder ID="PlaceHolderDashboardReport" runat="server"></asp:PlaceHolder>
                    </div> 
                </div>
            </asp:View>
        </asp:MultiView>
       
        </div>
        
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
