<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="NewLogin.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.NewLogin" Title="Transactional emails made easy for PayPal merchants" %>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div id="pagedescriptionbar">Login</div>
       
<br/>
<table>
        <tr>
            <td><asp:Label ID="lblMessage" runat="server" Text="Email"></asp:Label></td>
            <td style="padding-left:5px"> <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email"></asp:TextBox><br/></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label></td>
            <td  style="padding-left:5px"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Password"></asp:TextBox><br /></td>
        </tr>
        <tr>
            <td></td>
            <td style="padding-left:5px"><asp:Button ID="btnLogin" runat="server" CssClass="NormalButton" Text="Login" OnClick="btnLogin_Click" /></td>
        </tr>
    </table>
</asp:Content>