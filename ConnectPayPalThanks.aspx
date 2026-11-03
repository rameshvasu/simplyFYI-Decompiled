<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="ConnectPayPalThanks.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.ConnectPayPalThanks" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="instructioncontainer">                               
        <div class="instruction-big">PayPal and simplyFYI are connected!</div>
    </div>
    <br />PayPal and simplyFYI are connected to communicate with each other. You are all set to put simplyFYI into gear!
    <br /><br />
    Here are your next steps:
    <div style="padding-left:30px">
        <ol>
            <li>One.</li>
            <li>Two.</li>
            <li>Three.</li>                                
        </ol>
    </div>
        
    <asp:Button CssClass="NormalButtonVariableSize" runat="server" ID="btnContinue" Text="Go to the Control Panel" OnClick="btnContinue_Click" />
    
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>--%>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
