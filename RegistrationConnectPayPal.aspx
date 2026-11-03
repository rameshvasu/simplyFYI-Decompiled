<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="RegistrationConnectPayPal.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.RegistrationConnectPayPal" %>

<%--Oct 6, 2026: This script overrides compiled page code-behind for the new onboarding flow --%>
<script runat="server">
    
protected void Page_Load(object sender, EventArgs e)
    
{
        
	Response.Redirect("~/RegistrationThanks.aspx", true);
    
}

</script>


<%@ Register src="Controls/PayPalMerchantEmailForwarderSettings.ascx" tagname="PayPalMerchantEmailForwarderSettings" tagprefix="uc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <uc1:PayPalMerchantEmailForwarderSettings ID="PayPalMerchantEmailForwarderSettings1" runat="server" />
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
