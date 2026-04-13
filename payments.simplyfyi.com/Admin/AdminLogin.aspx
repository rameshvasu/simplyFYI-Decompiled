<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Admin.AdminLogin" Title="Admin Login" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div id="pagedescriptionbar">Administrator Login</div>
    <table>
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="Username"></asp:Label></td>
            <td><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox><br /></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="Password"></asp:Label></td>
            <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="155px"></asp:TextBox><br /></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="Button1" runat="server" CssClass="NormalButton" Text="Login" OnClick="Button1_Click" /></td>
        </tr>
    </table>
    
    
    
    
    
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>

