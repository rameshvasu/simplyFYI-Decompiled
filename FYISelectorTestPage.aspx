<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemplate.master" AutoEventWireup="true" CodeBehind="FYISelectorTestPage.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.FYISelectorTestPage" %>
<%@ Register src="Controls/PayPalSingleItemFYISelector.ascx" tagname="PayPalSingleItemFYISelector" tagprefix="uc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">   
      <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>    
    <uc1:PayPalSingleItemFYISelector ID="PayPalSingleItemFYISelector1" runat="server" />
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
