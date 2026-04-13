<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="ControlPanel.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Admin.ControlPanel" Title="Admin Control Panel" %>

<%@ Register Src="../Controls/ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
<div id="maincontent">        
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="pagedescriptionbar">Admin Control Panel</div>
    <fieldset>
        <legend>Customers</legend>
        <table>
        <tr>
            <td valign=middle><asp:DropDownList ID="DDCustomers" runat="server"></asp:DropDownList></td>
            <td valign=middle><asp:Button ID="Button1" runat="server" CssClass="NormalButton" Text="Open" OnClick="Button1_Click" /></td>
        </tr>
            <tr>
                <td>
                    <div style="padding-bottom:5px">
            <a href="./Customers.aspx">Enagage simplyFYI Payments customers</a>
            <br /><a href="./CustomFromEmail.aspx">Manage custom "from" email address</a>
         </div>
                </td>
            </tr>
    </table>    
    </fieldset> 
    <fieldset>
        <legend>Dispatch Service</legend>
        <table>
            <tr>
                <td>Last Run at</td>
                <td>
                    <asp:Label ID="lblLastRunDateTime" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="lblRunStatus" ForeColor="Green" runat="server" Text="OKAY"></asp:Label>
                </td>
            </tr>
            <tr>                
                <td>
                   <asp:TextBox ID="txtLogFileDate" runat="server"></asp:TextBox>                    
                   <cc1:CalendarExtender runat=server 
                       ID=LogFileCalendar 
                       TargetControlID=txtLogFileDate>                   
                   </cc1:CalendarExtender> 
                </td>
                <td>                    
                    <asp:Button ID="Button2" runat="server" CssClass="NormalButton" Text="View Log" OnClick="Button2_Click" />
                </td>
            </tr>            
        </table>
        <div style="text-align:left">
            <asp:GridView ID="GridView1" runat="server" CellPadding="10" CellSpacing="5"></asp:GridView>
        </div>

        
        
    </fieldset> 
         
    <fieldset style="display:none">
        <legend>PayPal IPN</legend>
        <table>
            <tr>
                <td><div>Transaction Date:</div></td>
                <td>
                    <asp:TextBox ID="txtIPNDate" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender 
                     runat=server
                     ID = IPNCalendar
                      TargetControlID=txtIPNDate>                                        
                    </cc1:CalendarExtender>
                </td>
                <td>
                    <asp:Button ID="btnGetIPNTransactions" runat="server" Text="Get Transactions" CssClass="NormalButton" OnClick="btnGetIPNTransactions_Click" /> 
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>  
                </td>
                <td>
                    <asp:DropDownList ID="DDIPNTransaction" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnViewIPNTransaction" runat="server" Text="View IPN" CssClass="NormalButton" OnClick="btnViewIPNTransaction_Click" />   
                </td>
            </tr>
        </table>
    </fieldset>

    <fieldset>
         <legend>Customer Activty</legend>
        <table>
            <tr>
                <td valign="top">
                    <label runat="server" id="MonthSummaryLabel">Month Summary</label>
                    <asp:GridView ID="GridView2" CellPadding="10" CellSpacing="5" runat="server">
                    </asp:GridView>

                </td>
                <td></td>
                <td valign="top">
                    <label runat="server" id="DailySummaryLabel">Daily Summary</label>
                    <asp:GridView ID="GridView3" CellPadding="10" CellSpacing="5" runat="server">
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </fieldset>
      
    <fieldset>
         <legend>Services</legend>
                <table>
                    <tr>
                        <td>
                            FYI PayPal Transaction Monitor Service
                        </td>
                        <td>
                            <span style="padding-left:10px"><asp:Button runat="server" ID="btnStartTransactionMonitorService" Text="Start" OnClick="btnStartTransactionMonitorService_Click" /></span>
                            <span style="padding-left:10px"><asp:Button runat="server" ID="btnStopTransactionMonitorService" Text="Stop" OnClick="btnStopTransactionMonitorService_Click" /></span>
                        </td>
                    </tr>
                </table>
    </fieldset>
</div>    
    <div style="display:none">
        <asp:Panel BorderColor=#dadada BorderStyle=solid BorderWidth=1px BackColor=#ffffff ID="IPNDisplayPanel" runat="server" Width="500px" Height=250px ScrollBars=Auto>
        <div style="background-color:#fafafa; font-weight:bold; border:solid 1px #dadada">IPN Values</div>
        </asp:Panel>
        <cc1:DragPanelExtender
         runat=server
         TargetControlID=IPNDisplayPanel
          DragHandleID=IPNDisplayPanel>
        </cc1:DragPanelExtender>
    </div>
    
    <fieldset style="display:none">
        <legend>Retry Errors</legend>
        
    </fieldset>

      
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
