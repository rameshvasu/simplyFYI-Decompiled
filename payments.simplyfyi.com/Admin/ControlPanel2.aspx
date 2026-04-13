<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="ControlPanel2.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Admin.ControlPanel2" Title="Untitled Page" %>

<%@ Register Src="~/Controls/ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="pagedescriptionbar">Admin Control Panel</div>
    <fieldset>
        <legend>Customers</legend>
        <table>
        <tr>
            <td valign=middle><asp:DropDownList ID="DDCustomers" runat="server"></asp:DropDownList></td>
            <td valign=middle><asp:Button ID="Button1" runat="server" CssClass="NormalButton" Text="Open" OnClick="Button1_Click" /></td>
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
                </td>
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
    </fieldset> 
    <fieldset>
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
    <uc1:ModalDialog ID="ModalDialog1" runat="server" />
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
