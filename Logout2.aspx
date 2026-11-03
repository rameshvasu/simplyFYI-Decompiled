<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="Logout2.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Logout2" Title="Transactional emails made easy for PayPal merchants" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">          
    <div style="text-align:left; margin:15px 15px 15px 15px">
        <asp:Label ID="lblMessage" runat="server" Text="Your session has ended."></asp:Label>   
    </div>
    <div style="text-align:left; margin-left:15px">
    Visit <a href="https://www.simplyfyi.com">simplyfyi.com</a> to learn more.
        <br />
        <br />
    </div>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
